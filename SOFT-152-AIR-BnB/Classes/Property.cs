using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOFT_152_AIR_BnB
{
    class Property
    {
        //
        //Defining private variables
        //
        private int propertyID;
        private string propertyName;
        private int hostID;
        private string hostName;
        private int numHostProperties;
        private double latitude;
        private double longitude;
        private string roomType;
        private double price;
        private int minNumNights;
        private int availability;

        //Empty constructor, used to create an empty property, then adding the data in later
        public Property()
        {
        }
        //
        //Constructor that takes in only strings - reading file and user-input
        //
        public Property(string inPropID, string inPropName, string inHostID, string inHostName, string inNumHostProp, string inLatitude, string inLongitude,
                        string inPrice, string inMinNumNights, string inAvailability, string inRoomType)
        {
            SetPropertyID(inPropID);
            SetPropertyName(inPropName);
            SetHostID(inHostID);
            SetHostName(inHostName);
            SetNumHostProperties(inNumHostProp);
            SetLatitude(inLatitude);
            SetLongitude(inLongitude);
            SetPrice(inPrice);
            SetMinNumNights(inMinNumNights);
            SetAvailability(inAvailability);
            SetRoomType(inRoomType);
        }
        //Gets & sets - Opening a message box in catch to use more accurate error messages

        public void SetPropertyID(string inPropID)
        {
            try
            {
                propertyID = Convert.ToInt32(inPropID);
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show(String.Format("Error in processing property ID {0} for property {1}", inPropID, propertyName));
            }
        }
        public int GetPropertyID()
        {
            return propertyID;
        }

        public void SetPropertyName(string inPropName)
        {
            propertyName = inPropName;
        }
        public string GetPropertyName()
        {
            return propertyName;
        }

        public void SetHostID(int inHostID)
        {
            hostID = inHostID;
        }
        public void SetHostID(string inHostID)
        {
            try
            {
                hostID = Convert.ToInt32(inHostID);
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show(String.Format("Error in processing host ID {0} for property{1}", inHostID, propertyName));
            }
        }
        public int GetHostID()
        {
            return hostID;
        }
        public string GetFormattedHostID()
        {
            return Convert.ToString(hostID);
        }

        public void SetHostName(string inHostName)
        {
            hostName = inHostName;
        }
        public string GetHostName()
        {
            return hostName;
        }
        public void SetRoomType(string inRoomType)
        {
            roomType = inRoomType;
        }
        public string GetRoomType()
        {
            return roomType;
        }


        public void SetNumHostProperties(int inNumHostProperties)
        {
            numHostProperties = inNumHostProperties;
        }
        public void SetNumHostProperties(string inNumHostProperties)
        {
            try
            {
                numHostProperties = Convert.ToInt32(inNumHostProperties);
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show(String.Format("Error in processing number of properties listed for host {0} for host {1}", inNumHostProperties, hostName));
            }
        }
        public int GetNumHostProperties()
        {
            return numHostProperties;
        }
        public string GetFormattedHostProperties()
        {
            try
            {
                return Convert.ToString(numHostProperties);
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show(String.Format("Error in formatting number of properties listed for host {0} for host {1}", numHostProperties, hostName));
                return null;
            }
        }

        public void SetLatitude(double inLatitude)
        {
            latitude = inLatitude;
        }
        public void SetLatitude(string inLatitude)
        {
            try
            {
                latitude = Convert.ToDouble(inLatitude);
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show(String.Format("Error in processing latitude {0} for {1}", inLatitude, propertyName));
            }
        }
        public double GetLatitude()
        {
            return latitude;
        }

        public void SetLongitude(double inLongitude)
        {
            longitude = inLongitude;
        }
        public void SetLongitude(string inLongitude)
        {
            try
            {
                longitude = Convert.ToDouble(inLongitude);
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show(String.Format("Error in processing longitude {0} for {1}", inLongitude, propertyName));
            }
        }
        public double GetLongitude()
        {
            return longitude;
        }
        public void SetPrice(double inPrice)
        {
            price = inPrice;
        }
        public void SetPrice(string inPrice)
        {
            try
            {
                price = Convert.ToDouble(inPrice);
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show(String.Format("Error in setting price {0} for property {1}", inPrice, propertyName));
            }
        }
        public double GetPrice()
        {
            return price;
        }

        public void SetMinNumNights(int inMinNumNights)
        {
            minNumNights = inMinNumNights;
        }
        public void SetMinNumNights(string inMinNumNights)
        {
            try
            {
                minNumNights = Convert.ToInt32(inMinNumNights);
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show(String.Format("Error processing the minimum number of nights ({0}) for property {1}", inMinNumNights, propertyName));
            }
        }
        public int GetMinNumNights()
        {
            return minNumNights;
        }

        public void SetAvailability(int inAvailability)
        {
            //Making sure that it cannot be avalible for more than a whole year during a leap year
            //Extra error checking to check if year is implimented can be added if this turns out to cause OBO errors
            if (inAvailability <= 366)
            {
                availability = inAvailability;
            }
            else
            {
                System.Windows.Forms.MessageBox.Show(String.Format("Error in inputting availability for {0}, a property cannot be available for more than a year!", propertyName));
            }
        }
        public void SetAvailability(string inAvailability)
        {
            try
            {
                int checkAvailability = Convert.ToInt32(inAvailability);
                //Making sure that it cannot be avalible for more than a whole year during a leap year
                if (checkAvailability <= 366)
                {
                    availability = checkAvailability;
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show(String.Format("Error in inputting availability for {0}, a property cannot be available for more than a year!", propertyName));
                }
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show(String.Format("Error processing availability {0} for property {1}", inAvailability, propertyName));
            }
        }
        public int GetAvailability()
        {
            return availability;
        }
    }
}
