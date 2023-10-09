﻿using System.Collections;
using NSubstitute;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace a_player
{
    public static class Helpers
    {
        public static IEnumerator LoadMovementTestsScene()
        {
            var operation = SceneManager.LoadSceneAsync("MovementTests");
            while (operation.isDone == false)
            {
                yield return null;
            }
        }

        public static Player GetPlayer()
        {
            Player player = GameObject.FindFirstObjectByType<Player>();

            var testPlayerInput = Substitute.For<IPlayerInput>();
            player.PlayerInput = testPlayerInput;
            return player;
        }

        public static float CalculateTurn(Quaternion originalRotation, Quaternion transformRotation)
        {
            var cross = Vector3.Cross(originalRotation * Vector3.forward, transformRotation * Vector3.forward);
            var dot = Vector3.Dot(cross, Vector3.up);
            return dot;
        }
    }
    public class with_positive_vertical_input
    {
        [UnityTest]
        public IEnumerator moves_forward()
        {
            yield return Helpers.LoadMovementTestsScene();

            var player = Helpers.GetPlayer();
            
            player.PlayerInput.Vertical.Returns(1f);

            float startingZPos = player.transform.position.z;
            
            yield return new WaitForSeconds(5f);

            float endingZPos = player.transform.position.z;
            Assert.Greater(endingZPos, startingZPos);
        }
    }
    
    public class with_negative_vertical_input
    {
        [UnityTest]
        public IEnumerator moves_forward()
        {
            yield return Helpers.LoadMovementTestsScene();

            var player = Helpers.GetPlayer();
            
            player.PlayerInput.Vertical.Returns(-1f);

            float startingZPos = player.transform.position.z;
            
            yield return new WaitForSeconds(5f);

            float endingZPos = player.transform.position.z;
            Assert.Less(endingZPos, startingZPos);
        }
    }

    public class with_negative_mouse_x
    {
        [UnityTest]
        public IEnumerator turns_left()
        {
            yield return Helpers.LoadMovementTestsScene();
            var player = Helpers.GetPlayer();

            player.PlayerInput.MouseX.Returns(-1f);

            var originalRotation = player.transform.rotation;
            yield return new WaitForSeconds(0.5f);

            float turnAmount = Helpers.CalculateTurn(originalRotation, player.transform.rotation);
            Assert.Less(turnAmount, 0);
        }
    }
    
    public class with_positive_mouse_x
    {
        [UnityTest]
        public IEnumerator turns_left()
        {
            yield return Helpers.LoadMovementTestsScene();
            var player = Helpers.GetPlayer();

            player.PlayerInput.MouseX.Returns(1f);

            var originalRotation = player.transform.rotation;
            yield return new WaitForSeconds(0.5f);

            float turnAmount = Helpers.CalculateTurn(originalRotation, player.transform.rotation);
            Assert.Greater(turnAmount, 0);
        }
    }

    public class moving_into_an_item
    {
        [UnityTest]
        public IEnumerator picks_up_and_equips_item()
        {
            yield return Helpers.LoadMovementTestsScene();

            var player = Helpers.GetPlayer();
            
            player.PlayerInput.Vertical.Returns(1f);

            Item item = Object.FindFirstObjectByType<Item>();
            
            Assert.AreNotSame(item, player.GetComponent<Inventory>().ActiveItem);
            
            yield return new WaitForSeconds(1f);

            Assert.AreSame(item, player.GetComponent<Inventory>().ActiveItem);
        }
    }
}