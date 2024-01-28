namespace Assets.Scripts
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class QuadTree<T>
    {
        private class QuadTreeNode
        {
            public Rect boundary;
            public List<T> items;
            public QuadTreeNode[] children;

            public QuadTreeNode(Rect boundary)
            {
                this.boundary = boundary;
                items = new List<T>();
                children = new QuadTreeNode[4];
            }
        }

        private QuadTreeNode root;
        private int maxItemsPerNode;

        public QuadTree(Rect boundary, int maxItemsPerNode)
        {
            root = new QuadTreeNode(boundary);
            this.maxItemsPerNode = maxItemsPerNode;
        }

        public void Insert(T item, Rect itemBounds)
        {
            Insert(root, item, itemBounds);
        }

        private void Insert(QuadTreeNode node, T item, Rect itemBounds)
        {
            if (!node.boundary.Contains(itemBounds.center))
                return;

            if (node.items.Count < maxItemsPerNode || node.children[0] == null)
            {
                node.items.Add(item);
            }
            else
            {
                if (node.children[0] == null)
                    SplitNode(node);

                for (int i = 0; i < 4; i++)
                {
                    if (node.children[i].boundary.Contains(itemBounds.center))
                    {
                        Insert(node.children[i], item, itemBounds);
                        break;
                    }
                }
            }
        }

        private void SplitNode(QuadTreeNode node)
        {
            float subWidth = node.boundary.width / 2f;
            float subHeight = node.boundary.height / 2f;
            float x = node.boundary.x;
            float y = node.boundary.y;

            node.children[0] = new QuadTreeNode(new Rect(x + subWidth, y, subWidth, subHeight));
            node.children[1] = new QuadTreeNode(new Rect(x, y, subWidth, subHeight));
            node.children[2] = new QuadTreeNode(new Rect(x, y + subHeight, subWidth, subHeight));
            node.children[3] = new QuadTreeNode(new Rect(x + subWidth, y + subHeight, subWidth, subHeight));

            // Move existing items to children
            foreach (T item in node.items)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (node.children[i].boundary.Contains(node.boundary.center))
                    {
                        node.children[i].items.Add(item);
                        break;
                    }
                }
            }

            node.items.Clear();
        }

        public List<T> Query(Rect range)
        {
            List<T> result = new List<T>();
            Query(root, range, result);
            return result;
        }

        private void Query(QuadTreeNode node, Rect range, List<T> result)
        {
            if (!node.boundary.Overlaps(range))
                return;

            foreach (T item in node.items)
            {
                if (range.Contains(node.boundary.center))
                    result.Add(item);
            }

            if (node.children[0] != null)
            {
                for (int i = 0; i < 4; i++)
                {
                    Query(node.children[i], range, result);
                }
            }
        }
    }

}