using AutoBogus;
using Xunit;
using Shouldly;
using ObjectCompare.Models;
using ObjectCompare.Entities;
using System.Collections.Generic;

namespace ObjectCompare.Test
{
    public class ComparerTests
    {
        [Fact]
        public void AllFieldsByFieldName_Equals()
        {
            PropertyModel model = EqualsModel;
            PropertyModel model2 = EqualsModel;

            new ObjectComparer<PropertyModel, PropertyModel>(model, model2)
                .Compare()
                .ShouldBeTrue();
        }

        [Fact]
        public void AllFieldsByFieldName_NotEquals()
        {
            PropertyModel model = RandomModel;
            IProperty entity = RandomEntity;

            new ObjectComparer<PropertyModel, IProperty>(model, entity)
                .Compare()
                .ShouldBeFalse();
        }

        [Fact]
        public void SomeFieldsByMapping_Equals()
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

            c.Compare()
             .ShouldBeTrue();
        }

        [Fact]
        public void SomeFieldsByMapping_NotEquals()
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

            c.Compare()
             .ShouldBeFalse();
        }

        PropertyModel RandomModel => new AutoFaker<PropertyModel>().Generate();
        IProperty RandomEntity => new AutoFaker<PropertyEntity>().Generate();

        PropertyModel EqualsModel => new PropertyModel
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

        IProperty EqualsEntity => new PropertyEntity
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
}
