using System;
using System.Collections.Generic;
using System.Text;

namespace Caristocrate.Common.Models
{
    public class CustMyCars
    {
        public class Request
        {
            public int categoryID { get; set; }
            public string makeID { get; set; }
            public string modelID { get; set; }
            public int minPrice { get; set; }
            public int maxPrice { get; set; }
            public int minYear { get; set; }
            public int maxYear { get; set; }
            public int minKM { get; set; }
            public int maxKM { get; set; }
            public string CarType { get; set; }
            public string Region { get; set; }
            public int sortbyYear { get; set; }
        }

        public class DimensionsWeight
        {
            public string name { get; set; }
            public string value { get; set; }
            public int? is_highlight { get; set; }
        }

        public class SeatingCapacity
        {
            public string name { get; set; }
            public string value { get; set; }
            public int? is_highlight { get; set; }
        }

        public class DRIVETRAIN
        {
            public string name { get; set; }
            public string value { get; set; }
            public int? is_highlight { get; set; }
        }

        public class Engine
        {
            public string name { get; set; }
            public string value { get; set; }
            public int? is_highlight { get; set; }
        }

        public class Performance
        {
            public string name { get; set; }
            public string value { get; set; }
            public int? is_highlight { get; set; }
        }

        public class Transmission
        {
            public string name { get; set; }
            public string value { get; set; }
            public int? is_highlight { get; set; }
        }

        public class Brake
        {
            public string name { get; set; }
            public string value { get; set; }
            public int? is_highlight { get; set; }
        }

        public class Suspension
        {
            public string name { get; set; }
            public string value { get; set; }
            public int? is_highlight { get; set; }
        }

        public class WheelsTyre
        {
            public string name { get; set; }
            public string value { get; set; }
            public int? is_highlight { get; set; }
        }

        public class Fuel
        {
            public string name { get; set; }
            public string value { get; set; }
            public int? is_highlight { get; set; }
        }

        public class Emission
        {
            public string name { get; set; }
            public string value { get; set; }
            public int? is_highlight { get; set; }
        }

        public class WarrantyMaintenance
        {
            public string name { get; set; }
            public string value { get; set; }
            public int? is_highlight { get; set; }
        }


        public class Drivetrain2
        {
            public string name { get; set; }
            public string value { get; set; }
            public int? is_highlight { get; set; }
        }

        public class LimitedEditionSpecsArray
        {
            public List<DimensionsWeight> Dimensions_Weight { get; set; }
            public List<SeatingCapacity> Seating_Capacity { get; set; }
            public List<DRIVETRAIN> DRIVE_TRAIN { get; set; }
            public List<Engine> Engine { get; set; }
            public List<Performance> Performance { get; set; }
            public List<Transmission> transmission { get; set; }
            public List<Brake> Brakes { get; set; }
            public List<Suspension> Suspension { get; set; }
            public List<WheelsTyre> Wheels_Tyres { get; set; }
            public List<Fuel> Fuel { get; set; }
            public List<Emission> Emission { get; set; }
            public List<WarrantyMaintenance> Warranty_Maintenance { get; set; }
            public List<Drivetrain2> Drivetrain { get; set; }
        }

        public class DepreciationTrendValue
        {
            public int? year { get; set; }
            public decimal? amount { get; set; }
        }

        public class Details
        {
            public int? id { get; set; }
            public string first_name { get; set; }
            public object last_name { get; set; }
            public int? is_verified { get; set; }
            public int? dealer_type { get; set; }
            public string dealer_type_text { get; set; }
            public string country_code { get; set; }
            public string phone { get; set; }
            public string address { get; set; }
            public string image { get; set; }
            public bool email_updates { get; set; }
            public bool social_login { get; set; }
            public int? region_reminder { get; set; }
            public string about { get; set; }
            public int? gender { get; set; }
            public string nationality { get; set; }
            public string profession { get; set; }
            public string dob { get; set; }
            public string image_url { get; set; }
            public string gender_label { get; set; }
        }

        public class ShowroomDetails
        {
            public int? id { get; set; }
            public string name { get; set; }
            public string address { get; set; }
            public string phone { get; set; }
            public string email { get; set; }
            public string about { get; set; }
            public string logo_url { get; set; }
        }

        public class Owner
        {
            public int? id { get; set; }
            public string name { get; set; }
            public string email { get; set; }
            public string created_at { get; set; }
            public int? push_notification { get; set; }
            public int? cars_count { get; set; }
            public int? favorite_count { get; set; }
            public int? like_count { get; set; }
            public int? view_count { get; set; }
            public Details details { get; set; }
            public ShowroomDetails showroom_details { get; set; }
        }

        public class BrandMedia
        {
            public int? id { get; set; }
            public int? media_type { get; set; }
            public string title { get; set; }
            public string filename { get; set; }
            public string created_at { get; set; }
            public string file_url { get; set; }
        }

        public class Brand
        {
            public int? id { get; set; }
            public string name { get; set; }
            public List<BrandMedia> brandmedia { get; set; }
        }

        public class CarModel
        {
            public int? id { get; set; }
            public string name { get; set; }
            public Brand brand { get; set; }
        }

        public class CarType
        {
            public int? id { get; set; }
            public object image { get; set; }
            public string name { get; set; }
        }

        public class Media
        {
            public int? id { get; set; }
            public int? media_type { get; set; }
            public string title { get; set; }
            public string filename { get; set; }
            public string created_at { get; set; }
            public string file_url { get; set; }
        }

        public class RegionalSpecs
        {
            public int? id { get; set; }
            public string created_at { get; set; }
            public string name { get; set; }
        }

        public class CategoryMedia
        {
            public int? id { get; set; }
            public int? media_type { get; set; }
            public string title { get; set; }
            public string filename { get; set; }
            public string created_at { get; set; }
            public string file_url { get; set; }
        }

        public class Category
        {
            public int? id { get; set; }
            public string slug { get; set; }
            public int? user_id { get; set; }
            public int? type { get; set; }
            public string created_at { get; set; }
            public string updated_at { get; set; }
            public int? unread_count { get; set; }
            public int? clicks_count { get; set; }
            public string name { get; set; }
            public string subtitle { get; set; }
            public string description { get; set; }
            public List<CategoryMedia> categorymedia { get; set; }
            public List<object> child_category { get; set; }
        }

        public class EngineType
        {
            public int? id { get; set; }
            public string name { get; set; }
        }

        public class Data
        {
            public int? id { get; set; }
            public int? category_id { get; set; }
            public int? version_id { get; set; }
            string version_app { get; set; }
            decimal? average_rating { get; set; }
            public int? views_count { get; set; }
            public int? favorite_count { get; set; }
            public int? like_count { get; set; }
            public int? year { get; set; }
            public string chassis { get; set; }
            public int? kilometer { get; set; }
            public decimal? average_mkp { get; set; }
            public decimal? amount { get; set; }
            public string currency { get; set; }
            public string name { get; set; }
            public string email { get; set; }
            public string country_code { get; set; }
            public int? phone { get; set; }
            public string notes { get; set; }
            public string life_cycle { get; set; }
            public int? status { get; set; }
            public int? is_featured { get; set; }
            public string bid_close_at { get; set; }
            public string created_at { get; set; }
            public string transmission_type_text { get; set; }
            public string link { get; set; }
            public string status_text { get; set; }
            public string owner_type_text { get; set; }
            public List<object> top_bids { get; set; }
            public bool is_liked { get; set; }
            public bool is_viewed { get; set; }
            public bool is_favorite { get; set; }
            public LimitedEditionSpecsArray limited_edition_specs_array { get; set; }
            public List<DepreciationTrendValue> depreciation_trend_value { get; set; }
            public string ref_num { get; set; }
            public Owner owner { get; set; }
            public CarModel car_model { get; set; }
            public CarType car_type { get; set; }
            public List<Media> media { get; set; }
            public List<MyCarAttributes> my_car_attributes { get; set; }
            public RegionalSpecs regional_specs { get; set; }
            public List<CarRegions> car_regions { get; set; }
            public Category category { get; set; }
            public List<Review> reviews { get; set; }
            public List<Dealer> dealers { get; set; }
            public EngineType engine_type { get; set; }
            public object version { get; set; }
            public Errors errors { get; set; }
        }

        public class Review
        {
            public int? id { get; set; }
            public double average_rating { get; set; }
            public string review_message { get; set; }
            public UserDetails user_details { get; set; }
            public List<Detail> details { get; set; }
        }

        public class UserDetails
        {
            public int? user_id { get; set; }
            public string user_name { get; set; }
            public string image_url { get; set; }
        }

        public class Detail
        {
            public int? rating { get; set; }
            public int? aspect_id { get; set; }
            public string aspect_title { get; set; }
        }

        public class Dealer
        {
            public int? id { get; set; }
            public string name { get; set; }
            public string email { get; set; }
            public int? status { get; set; }
            public string created_at { get; set; }
            public int? push_notification { get; set; }
            public int? cars_count { get; set; }
            public int? favorite_count { get; set; }
            public int? like_count { get; set; }
            public int? view_count { get; set; }
            public Details2 details { get; set; }
            public ShowroomDetails2 showroom_details { get; set; }
        }

        public class Details2
        {
            public int? id { get; set; }
            public string first_name { get; set; }
            public object last_name { get; set; }
            public int? is_verified { get; set; }
            public int? dealer_type { get; set; }
            public string dealer_type_text { get; set; }
            public string country_code { get; set; }
            public string phone { get; set; }
            public string address { get; set; }
            public string image { get; set; }
            public bool email_updates { get; set; }
            public bool social_login { get; set; }
            public int? region_id { get; set; }
            public int? region_reminder { get; set; }
            public int? limit_for_cars { get; set; }
            public int? limit_for_featured_cars { get; set; }
            public string expiry_date { get; set; }
            public string about { get; set; }
            public int? gender { get; set; }
            public string nationality { get; set; }
            public string profession { get; set; }
            public string dob { get; set; }
            public string image_url { get; set; }
            public string gender_label { get; set; }
            public RegionDetail2 region_detail { get; set; }
        }

        public class RegionDetail2
        {
            public int? id { get; set; }
            public string name { get; set; }
            public string currency { get; set; }
            public int? is_my_region { get; set; }
            public string flag { get; set; }
        }


        public class ShowroomDetails2
        {
            public int? id { get; set; }
            public string name { get; set; }
            public string address { get; set; }
            public string phone { get; set; }
            public string email { get; set; }
            public string about { get; set; }
            public string logo_url { get; set; }
        }

        public class MyCarAttributes
        {
            public string value { get; set; }
            public int? attr_id { get; set; }
            public string attr_name { get; set; }
            public string attr_icon { get; set; }
            public string attr_option { get; set; }
        }

        public class Region
        {
            public int? id { get; set; }
            public string name { get; set; }
            public int? is_my_region { get; set; }
            public string flag { get; set; }
        }

        public class CarRegions
        {
            public decimal price { get; set; }
            public Region region { get; set; }
        }


        public class Response
        {
            public bool success { get; set; }
            public List<Data> data { get; set; }
            public string message { get; set; }
        }

        public class ResponsebyID
        {
            public bool success { get; set; }
            public Data data { get; set; }
            public string message { get; set; }
        }

        public class Errors
        {
            public List<string> token { get; set; }
        }
    }
}
