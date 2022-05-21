using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.Entities
{
    public class AdvertisementAppUser:BaseEntity
    {
        public int AdvertisementId { get; set; }
        public int AppUserId { get; set; }
        public int AdvertisementAppUserStatusId { get; set; }
        public int MilitaryStatusId { get; set; }
        public DateTime? EndDate { get; set; }
        public int WorkExperience { get; set; }
        public string CVPath { get; set; }

        //Navigation properties
        public Advertisement Advertisement { get; set; }
        public AppUser AppUser { get; set; }
        public AdvertisementAppUserStatus AdvertisementAppUserStatus { get; set; }
        public MilitaryStatus MilitaryStatus { get; set; }

    }
}
