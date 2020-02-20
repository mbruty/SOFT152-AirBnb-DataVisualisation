using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace SOFT_152_AIR_BnB
{
    static class FileManager
    {
        public static void ReadDataIn(string inPath, ref Data data)
        {
            using (StreamReader reader = new StreamReader(inPath))
            {
                while (!reader.EndOfStream)
                {
                    District currentDistrict = new District(reader.ReadLine(), reader.ReadLine());
                    //Loop through the neighbourhoods in the district
                    for (int i = 0; i < currentDistrict.GetNumNeighbourhoods(); i++)
                    {
                        Neighbourhood currentNeighbourhood = new Neighbourhood(reader.ReadLine(), reader.ReadLine());
                        //Loop through each property in neighbourhood
                        for (int j = 0; j < currentNeighbourhood.GetNumProperties(); j++)
                        {
                            Property currentProperty = new Property();
                            
                            //Set each element of the property
                            currentProperty.SetPropertyID(reader.ReadLine());
                            currentProperty.SetPropertyName(reader.ReadLine());
                            currentProperty.SetHostID(reader.ReadLine());
                            currentProperty.SetHostName(reader.ReadLine());
                            currentProperty.SetNumHostProperties(reader.ReadLine());
                            currentProperty.SetLatitude(reader.ReadLine());
                            currentProperty.SetLongitude(reader.ReadLine());
                            currentProperty.SetRoomType(reader.ReadLine());
                            currentProperty.SetPrice(reader.ReadLine());
                            currentProperty.SetMinNumNights(reader.ReadLine());
                            currentProperty.SetAvailability(reader.ReadLine());


                            //Add the property to the neighbourhood array
                            currentNeighbourhood.AddProperty(currentProperty, j);
                        }
                        //Add the neighbourhood to the district array
                        currentDistrict.AddNeighbourhood(currentNeighbourhood, i);
                        //Set the reading data in done for that nbhood
                        currentNeighbourhood.ReadingDone();
                        currentNeighbourhood.CalculateAverage();
                    }
                    //Set reading data in done for that district
                    currentDistrict.ReadingDone();
                    currentDistrict.CalculateAverage();
                    //Add the district to the data array
                    data.AddDistrict(currentDistrict);

                }
            }//Set break point on this line to inspect the data
        }
        public static void SaveDataOut(string outPath, Data data)
        {
            //The oposite of reading data in, so loops are the same
            using (StreamWriter writer = new StreamWriter(outPath))
            {
                foreach (District district in data.GetAllDistricts())
                {
                    writer.WriteLine(district.GetDistrictName());
                    writer.WriteLine(district.GetNumNeighbourhoods());
                    foreach (Neighbourhood nbHood in district.GetAllNeighbourhoods())
                    {
                        writer.WriteLine(nbHood.GetNeighbourhoodName());
                        writer.WriteLine(nbHood.GetNumProperties());
                        foreach (Property property in nbHood.GetAllProperties())
                        {
                            writer.WriteLine(property.GetPropertyID());
                            writer.WriteLine(property.GetPropertyName());
                            writer.WriteLine(property.GetHostID());
                            writer.WriteLine(property.GetHostName());
                            writer.WriteLine(property.GetNumHostProperties());
                            writer.WriteLine(property.GetLatitude());
                            writer.WriteLine(property.GetLongitude());
                            writer.WriteLine(property.GetRoomType());
                            writer.WriteLine(property.GetPrice());
                            writer.WriteLine(property.GetMinNumNights());
                            writer.WriteLine(property.GetAvailability());
                        }
                    }
                }
            }
        }
    }
}
