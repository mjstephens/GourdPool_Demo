using System.Collections.Generic;

namespace DefaultNamespace
{
    public static class ObjectCollection
    {
        public static readonly List<PooledObjectInstance> collection = new List<PooledObjectInstance>();

        public static void OnToggle()
        {
            for (int i = collection.Count - 1; i >= 0; i--)
            {
                collection[i].DestroyFromCollection();
            }
        }
    }
}