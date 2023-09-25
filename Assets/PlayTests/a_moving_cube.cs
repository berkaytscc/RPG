using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class a_moving_cube
    {
        [UnityTest]
        public IEnumerator a_moving_changes_position()
        {
            // ARRANGE
            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = Vector3.zero;

            // ACT
            for (int i = 0; i < 10; i++)
            {
                cube.transform.position += Vector3.forward;
                yield return new WaitForSeconds(0.5f);
            }

            // ASSERT
            Assert.AreEqual(10, cube.transform.position.z);

            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }   
}
