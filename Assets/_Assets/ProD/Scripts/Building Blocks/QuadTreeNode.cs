//This is more like a struct. It's used by QuadTreeMapGenerator.cs

using UnityEngine;
using System.Collections;

namespace ProD
{
	public class QuadTreeNode 
	{
		public readonly QuadTreeNode[] children = new QuadTreeNode[4];
		
		public QuadTreeNode childNW;
		public QuadTreeNode childNE;
		public QuadTreeNode childSW;
		public QuadTreeNode childSE;
		
		public int start_X = 0;
		public int start_Y = 0;
		public int end_X = 0;
		public int end_Y = 0;
		
		public int width = 0;
		public int height = 0;
		
		public int nodeDepth = 0;
		
		public QuadTreeNode(int start_X, int start_Y, int end_X, int end_Y, int nodeDepth)
		{
			this.start_X = start_X;
			this.start_Y = start_Y;
			this.end_X = end_X;
			this.end_Y = end_Y;
			this.width = end_X + 1 - start_X;
			this.height = end_Y + 1 - start_Y;
			this.nodeDepth = nodeDepth;
		}
	}
}
