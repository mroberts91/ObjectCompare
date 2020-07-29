using System;
using System.Collections.Generic;
using System.Linq;

namespace ObjectCompare
{
    public class ObjectComparer<T1, T2>
    {

        private readonly T1 Object1;
        private readonly T2 Object2;

        public List<ValuePair> Pairs { get; set; } = new List<ValuePair>();


        /// <summary>
        /// Initializes an ObjectComparer that compares all the field values of the two supplied objects
        /// </summary>
        /// <param name="object1"></param>
        /// <param name="object2"></param>
        public ObjectComparer(T1 object1, T2 object2)
        {
            Object1 = object1;
            Object2 = object2;
            var obj1Fields = object1.GetType().GetProperties().ToList();
            var obj2Fields = object2.GetType().GetProperties().ToList();

            foreach (var obj1Prop in obj1Fields)
            {
                var obj2Prop = obj2Fields.Where(p => p.Name == obj1Prop.Name).FirstOrDefault();
                if (obj1Prop != null && obj2Prop != null)
                    Pairs.Add(
                        ValuePair.Create(
                                                Object1.GetType().GetProperty(obj1Prop.Name).GetValue(Object1),
                                                Object2.GetType().GetProperty(obj2Prop.Name).GetValue(Object2)
                    ));
            }
        }

        /// <summary>
        /// Initializes an ObjectComparer that compares the two objects and defined mapped values supplied in 
        /// the configuration.
        /// </summary>
        public ObjectComparer(Action<ObjectComparerConfiguration> configuration)
        {
            var config = new ObjectComparerConfiguration
            {
                Object1 = (T1)(object)null,
                Object2 = (T2)(object)null,
                Mappings = new List<ValuePair>()
            };

            configuration.Invoke(config);

            Object1 = config.Object1;
            Object2 = config.Object2;
            Pairs.AddRange(config.Mappings.Select(pair => ValuePair.Create(pair.Prop1, pair.Prop2)));
        }

        public bool Compare()
        {
            return Pairs.All(p => p.ValuesEqual());
        }


        public class ObjectComparerConfiguration
        {
            public T1 Object1 { get; set; }
            public T2 Object2 { get; set; }
            public IEnumerable<ValuePair> Mappings { get; set; }
        }

    }
    public readonly struct ValuePair
    {
        public static ValuePair Create<T>(T prop1, T prop2)
        {
            return new ValuePair(prop1, prop2);
        }
        private ValuePair(object prop1, object prop2)
        {
            Prop1 = prop1;
            Prop2 = prop2;
        }

        internal object Prop1 { get; }
        internal object Prop2 { get; }

        internal bool ValuesEqual()
        {
            if (Prop1 is null)
                return Prop2 is null;

            if (Prop2 is null)
                return false;

            return Prop1 == Prop2;
        }
    }

}
