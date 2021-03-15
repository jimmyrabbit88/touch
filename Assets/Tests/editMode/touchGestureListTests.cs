using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class touchGestureListTests
    {
        // A Test behaves as an ordinary method
        [Test]
        public void touchGestureListTestsSimplePasses()
        {
            Touch touchA = new Touch();
            touchA.position = new Vector2(1648.6f, 922.0f);
            touchA.fingerId = 0;
            touchA.phase = TouchPhase.Moved;


            Touch touchB = new Touch();
            touchB.position = new Vector2(130.6f, 922.0f);
            touchB.fingerId = 1;
            touchB.phase = TouchPhase.Moved;

            Touch t1 = new Touch();
            t1.position = new Vector2(1700f, 922f);
            t1.fingerId = 0;

            Touch t2 = new Touch();
            t2.position = new Vector2(120f, 922f);
            t2.fingerId = 0;

            var tl1 = new List<Touch>();
            tl1.Add(t1);
            tl1.Add(t2);

            var tl2 = new List<Touch>();
            tl2.Add(t2);
            tl2.Add(t1);
            //List<TouchRes> tg2 = new List<TouchRes>();

            var tlres = new List<Touch>();
            tlres.Add(t2);
            tlres.Add(touchA);

            List<TouchResult> resList = new List<TouchResult>();
            resList.Add(new TouchResult(0, 3, tlres));
            resList.Add(new TouchResult(1, 3, tl2));


            //var tl = new List<Touch>();
            //tl.Add(touch);
            //touchGestureList.Add(new TouchRes(touch.fingerId, gesture, tl));
            
            List<TouchResult> tgl = new List<TouchResult>();
            tgl.Add(new TouchResult(0, 3, tl1));
            tgl.Add(new TouchResult(1, 3, tl2));


            var g = new GestureDetectorLocking();
            g.touchGestureList = tgl;
            var actual = g.UpdateListTouches(touchA, touchA);

            
            Assert.AreEqual(resList[0].Type, actual[0].Type);
            Assert.AreEqual(resList[0].FingerId, actual[0].FingerId);
            Assert.AreEqual(resList[0].Touch[0], actual[0].Touch[0]);
            Assert.AreEqual(resList[0].Touch[1], actual[0].Touch[1]);
            Assert.AreEqual(resList[0].Touch.Count, actual[0].Touch.Count);
            Assert.AreEqual(resList[0].Touch, actual[0].Touch);





            Assert.AreEqual(resList[1].Type, actual[1].Type);
            Assert.AreEqual(resList[1].FingerId, actual[1].FingerId);
            Assert.AreEqual(resList[1].Touch[0], actual[1].Touch[0]);
            Assert.AreEqual(resList[1].Touch[1], actual[1].Touch[1]);
            Assert.AreEqual(resList[1].Touch.Count, actual[1].Touch.Count);
            Assert.AreEqual(resList[1].Touch, actual[1].Touch);



        }
    }
}
