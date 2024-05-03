using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalFacultySystem.Entities;

public class MyFieldsMapper
{
    //generic for all classes
    public static T MapFields<T>(T source, T destination)
    {
        var sourceProperties = source.GetType().GetProperties();
        var destinationProperties = destination.GetType().GetProperties();

        foreach (var sourceProperty in sourceProperties)
        {
            foreach (var destinationProperty in destinationProperties)
            {
                if (sourceProperty.Name == destinationProperty.Name)
                {
                    destinationProperty.SetValue(destination, sourceProperty.GetValue(source));
                    break;
                }
            }
        }

        return destination;
    }
}
