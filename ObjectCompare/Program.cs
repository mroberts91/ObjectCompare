using System;
using AutoBogus;
using System.Collections.Generic;

namespace ObjectCompare
{
    class Program
    {
        static void Main(string[] args)
        {
            EqualsTestSomeFields();
            Console.WriteLine("----------------------");

            EqualsTestAllFields();
            Console.WriteLine("----------------------");

            NotEqualsTestSomeFields();
            Console.WriteLine("----------------------");

            NotEqualsTestAllFields();
            Console.WriteLine("----------------------");

            Console.WriteLine();
            Console.WriteLine("Press Enter to Exit...");
            Console.ReadLine();
        }


        static void EqualsTestAllFields()
        {
            PropertyModel model = EqualsModel;
            IProperty entity = EqualsEntity;

            var c = new ObjectComparer<PropertyModel, IProperty>(model, entity);

            Console.WriteLine($"{nameof(EqualsTestAllFields)}: Objects Are Equal? = {c.Compare()}");
        }

        static void EqualsTestSomeFields()
        {
            PropertyModel model = EqualsModel;
            IProperty entity = EqualsEntity;

            var c = new ObjectComparer<PropertyModel, IProperty>(config =>
            {
                config.Object1 = model;
                config.Object2 = entity;
                config.Mappings = new List<ValuePair>
                {
                    ValuePair.Create(model.AddressLine1, entity.AddressLine1),
                    ValuePair.Create(model.AddressLine2, entity.AddressLine2),
                    ValuePair.Create(model.Phase, entity.Phase),
                    ValuePair.Create(model.Id, entity.EntityId),
                };
            });

            Console.WriteLine($"{nameof(NotEqualsTestSomeFields)}: Objects Are Equal? = {c.Compare()}");
        }

        static void NotEqualsTestAllFields()
        {
            PropertyModel model = RandomModel;
            IProperty entity = RandomEntity;

            var c = new ObjectComparer<PropertyModel, IProperty>(model, entity);

            Console.WriteLine($"{nameof(NotEqualsTestAllFields)}: Objects Are Equal? = {c.Compare()}");
        }

        static void NotEqualsTestSomeFields()
        {
            PropertyModel model = RandomModel;
            IProperty entity = RandomEntity;

            var c = new ObjectComparer<PropertyModel, IProperty>(config =>
            {
                config.Object1 = model;
                config.Object2 = entity;
                config.Mappings = new List<ValuePair>
                {
                    ValuePair.Create(model.AddressLine1, entity.AddressLine1),
                    ValuePair.Create(model.AddressLine2, entity.AddressLine2),
                    ValuePair.Create(model.Phase, entity.Phase),
                    ValuePair.Create(model.Id, entity.EntityId),
                };
            });

            Console.WriteLine($"{nameof(NotEqualsTestSomeFields)}: Objects Are Equal? = {c.Compare()}");
        }

        static PropertyModel RandomModel => new AutoFaker<PropertyModel>().Generate();
        static IProperty RandomEntity => new AutoFaker<PropertyEntity>().Generate();

        static PropertyModel EqualsModel => new PropertyModel
        {
            Id = 10,
            AddressLine1 = "foobar",
            AddressLine2 = "barfoo",
            Acreage = "10.2",
            Block = null,
            City = "The City",
            County = "The County",
            LegalDescription = "some desc",
            Phase = "Two",
            State = "NC",
            Zip = "27603"
        };

        static IProperty EqualsEntity => new PropertyEntity
        {
            EntityId = 10,
            AddressLine1 = "foobar",
            AddressLine2 = "barfoo",
            Acreage = "10.2",
            Block = null,
            City = "The City",
            County = "The County",
            LegalDescription = "some desc",
            Phase = "Two",
            State = "NC",
            Zip = "27603"
        };
    }

    class PropertyModel
    {
        public int Id { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string County { get; set; }
        public string LegalDescription { get; set; }
        public string Acreage { get; set; }
        public string Block { get; set; }
        public string Phase { get; set; }
    }

    interface IProperty
    {
        string Acreage { get; set; }
        string AddressLine1 { get; set; }
        string AddressLine2 { get; set; }
        string Block { get; set; }
        string City { get; set; }
        string County { get; set; }
        int EntityId { get; set; }
        string LegalDescription { get; set; }
        string Phase { get; set; }
        string State { get; set; }
        string Zip { get; set; }
    }

    class PropertyEntity : IProperty
    {
        public int EntityId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string County { get; set; }
        public string LegalDescription { get; set; }
        public string Acreage { get; set; }
        public string Block { get; set; }
        public string Phase { get; set; }
    }
}
