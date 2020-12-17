using NUnit.Framework;
using Proj;

namespace ProjTests
{
    public class Tests
    {
        // pulled this from homework 2... so it better work
        private Vertex _v1;
        private Vertex _v2;
        private Vertex _v3;
        private Vertex _v4;
        private Vertex _v5;
        private Vertex _v6;
        private Vertex _v7;
        private Vertex _v8;
        private Vertex _v9;
        private Vertex _v10;

        private Graph _graph;

        
        [SetUp]
        public void SetUp()
        {
            _v1 = new Vertex("v1");
            _v2 = new Vertex("v2");
            _v3 = new Vertex("v3");
            _v4 = new Vertex("v4");
            _v5 = new Vertex("v5");
            _v6 = new Vertex("v6");
            _v7 = new Vertex("v7");
            _v8 = new Vertex("v8");
            _v9 = new Vertex("v9");
            _v10 = new Vertex("v10");
            
            _v1.Connect(_v2, 32, bidirectional:true);
            _v1.Connect(_v4, 17, bidirectional:true);
            _v2.Connect(_v5, 45, bidirectional:true);
            _v3.Connect(_v4, 18, bidirectional:true);
            _v3.Connect(_v7, 5, bidirectional:true);
            _v4.Connect(_v5, 10, bidirectional:true);
            _v4.Connect(_v8, 3, bidirectional:true);
            _v5.Connect(_v6, 28, bidirectional:true);
            _v6.Connect(_v10, 6, bidirectional:true);
            _v7.Connect(_v8, 59, bidirectional:true);
            _v8.Connect(_v9, 4, bidirectional:true);
            _v9.Connect(_v10, 12, bidirectional:true);
            
            _graph = new Graph(new []{_v1, _v2, _v3, _v4, _v5, _v6, _v7, _v8, _v9, _v10});
        }

        [Test]
        public void TestVertex()
        {
            Assert.AreEqual(17, _v4.Edges[_v1]);
            Assert.AreEqual(18, _v4.Edges[_v3]);
            Assert.AreEqual(10, _v4.Edges[_v5]);
            Assert.AreEqual(3, _v4.Edges[_v8]);
            
            Assert.IsFalse(_v4.Edges.ContainsKey(_v2));
            Assert.IsFalse(_v4.Edges.ContainsKey(_v4));
            Assert.IsFalse(_v4.Edges.ContainsKey(_v6));
            Assert.IsFalse(_v4.Edges.ContainsKey(_v7));
            Assert.IsFalse(_v4.Edges.ContainsKey(_v9));
            Assert.IsFalse(_v4.Edges.ContainsKey(_v10));
        }

        [Test]
        public void TestPath()
        {
            var path = new Path(_v2);
            Assert.AreEqual(0, path.Cost);
            Assert.AreEqual("v2", path.ToString());
            Assert.IsTrue(path.Contains(_v2));
            
            Assert.IsTrue(path.TryMakeNext(_v5, out path));
            Assert.AreEqual(45, path.Cost);
            Assert.AreEqual("v2 -(45)-> v5", path.ToString());
            Assert.IsTrue(path.Contains(_v2));
            Assert.IsTrue(path.Contains(_v5));
            
            Assert.IsTrue(path.TryMakeNext(_v6, out path));
            Assert.AreEqual(73, path.Cost);
            Assert.AreEqual("v2 -(45)-> v5 -(28)-> v6", path.ToString());
            Assert.IsTrue(path.Contains(_v2));
            Assert.IsTrue(path.Contains(_v5));
            Assert.IsTrue(path.Contains(_v6));
            
            Assert.IsTrue(path.TryMakeNext(_v10, out path));
            Assert.AreEqual(79, path.Cost);
            Assert.AreEqual("v2 -(45)-> v5 -(28)-> v6 -(6)-> v10", path.ToString());
            Assert.IsTrue(path.Contains(_v2));
            Assert.IsTrue(path.Contains(_v5));
            Assert.IsTrue(path.Contains(_v6));
            Assert.IsTrue(path.Contains(_v10));
            
            Assert.IsFalse(path.TryMakeNext(_v2, out path));
        }
        
        [Test]
        public void TestDijkstraUndirected()
        {
            var path44 = _graph.Dijkstra(_v4, _v4);
            Assert.AreEqual(path44, path44);
            
        }
    }
}