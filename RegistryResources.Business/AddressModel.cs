using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RegistryResources.Business
{
    public class AddressModel : ICloneable
    {
        [Key]
        public int AddressId { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public double Latitide { get; set; }
        public double Longitude { get; set; }
        public bool BadAddress { get; set; }
        public bool NoMail { get; set; }
        public bool NeedsVerification { get; set; }

        [NotMapped]
        public string FormattedAddress
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"{Street1}\r\n");
                if (!string.IsNullOrEmpty(Street2))
                {
                    sb.Append($"{Street2}\r\n");
                }
                sb.Append($"{City}, {State} {PostalCode}");
                return sb.ToString();
            }
        }

        public object Clone()
        {
            AddressModel clonedAddress = new AddressModel()
            {
                BadAddress = this.BadAddress,
                City = this.City,
                Latitide = this.Latitide,
                Longitude = this.Longitude,
                NeedsVerification = this.NeedsVerification,
                NoMail = this.NoMail,
                PostalCode = this.PostalCode,
                State = this.State,
                Street1 = this.Street1,
                Street2 = this.Street2
            };

            return clonedAddress;

        }

        //public int RegistrantId { get; set; }
        //public RegistrantModel Registrant { get; set; }

        public double DistanceTo(AddressModel addr, char unit)
        {
            double theta = this.Longitude - addr.Longitude;
            double dist = Math.Sin(deg2rad(this.Latitide)) * Math.Sin(deg2rad(addr.Latitide)) +
                Math.Cos(deg2rad(this.Latitide)) * Math.Cos(deg2rad(addr.Latitide)) * Math.Cos(deg2rad(theta));
            dist = Math.Acos(dist);
            dist = rad2deg(dist);
            dist = dist * 60 * 1.1515;
            if (unit == 'K')
            {
                dist = dist * 1.609344;
            }
            else if (unit == 'N')
            {
                dist = dist * 0.8684;
            }
            return (dist);
        }

        /*:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::*/
        /*::  This function converts decimal degrees to radians             :*/
        /*:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::*/
        private double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        /*:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::*/
        /*::  This function converts radians to decimal degrees             :*/
        /*:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::*/
        private double rad2deg(double rad)
        {
            return (rad * 180.0 / Math.PI);
        }
    }
}
