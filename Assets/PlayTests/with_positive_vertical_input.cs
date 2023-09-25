using System.Collections;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace a_player
{
    public class with_positive_vertical_input
    {
        [UnityTest]
        public IEnumerator moves_forward()
        {
            var floor = GameObject.CreatePrimitive(PrimitiveType.Cube);
            floor.transform.localScale = new Vector3(50, 0, 50);
            floor.transform.position = Vector3.zero;

            var playerGameObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            playerGameObject.AddComponent<CharacterController>();
            playerGameObject.transform.position = new Vector3(0, 1.3f, 0);
            
            Player player = playerGameObject.AddComponent<Player>();

            var testPlayerInput = Substitute.For<IPlayerInput>();
            player.PlayerInput = testPlayerInput;
            
            testPlayerInput.Vertical.Returns(1f);

            float startingZPos = player.transform.position.z;
            
            yield return new WaitForSeconds(5f);

            float endingZPos = player.transform.position.z;
            Assert.Greater(endingZPos, startingZPos);
        }
    }
    
    public class TestPlayerInput : IPlayerInput
    {
        public float Vertical { get; set; }
    }
}