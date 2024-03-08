using System;
using AutoFixture;
using AutoFixture.Kernel;
using System.Linq;
using System.Reflection;

public class OmitNestedPropertiesCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customizations.Add(new OmitNestedPropertiesSpecimenBuilder());
    }

    private class OmitNestedPropertiesSpecimenBuilder : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            var propertyInfo = request as PropertyInfo;
            if (propertyInfo == null)
                return new NoSpecimen();

            if (propertyInfo.PropertyType.IsClass && propertyInfo.PropertyType != typeof(string))
                return new OmitSpecimen();

            return context.Resolve(propertyInfo.PropertyType);
        }
    }
}
