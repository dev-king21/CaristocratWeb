using System;
using System.Collections.Generic;
using System.Text;

namespace Caristocrate.Common.Models
{
    public class CustTradeInByIDModel
    {
        public class BidCloseAt
        {
            public string date { get; set; }
            public int? timezone_type { get; set; }
            public string timezone { get; set; }
        }

        public class RegionDetail
        {
            public int? id { get; set; }
            public string name { get; set; }
            public string currency { get; set; }
            public int? is_my_region { get; set; }
            public string flag { get; set; }
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
            public RegionDetail region_detail { get; set; }
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

        public class DealerInfo
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
            public Details details { get; set; }
            public ShowroomDetails showroom_details { get; set; }
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

        public class LimitedEditionSpecsArray
        {
            public List<DimensionsWeight> Dimensions_Weight { get; set; }
            public List<SeatingCapacity> Seating_Capacity { get; set; }
            public List<DRIVETRAIN> DRIVE_TRAIN { get; set; }
            public List<Engine> Engine { get; set; }
            public List<Performance> Performance { get; set; }
            public List<Transmission> __invalid_name__Transmission { get; set; }
            public List<Brake> Brakes { get; set; }
            public List<Suspension> Suspension { get; set; }
            public List<WheelsTyre> Wheels_Tyres { get; set; }
            public List<Fuel> Fuel { get; set; }
            public List<Emission> Emission { get; set; }
            public List<WarrantyMaintenance> Warranty_Maintenance { get; set; }
        }

        public class DepreciationTrendValue
        {
            public int? year { get; set; }
            public decimal? amount { get; set; }
        }

        public class RegionDetail2
        {
            public int? id { get; set; }
            public string name { get; set; }
            public string currency { get; set; }
            public int? is_my_region { get; set; }
            public string flag { get; set; }
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

        public class Owner
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

        public class Medium
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
            public string slug { get; set; }
            public List<Medium> media { get; set; }
        }

        public class CarModel
        {
            public int? id { get; set; }
            public string name { get; set; }
            public string slug { get; set; }
            public Brand brand { get; set; }
        }

        public class CarType
        {
            public int? id { get; set; }
            public string image { get; set; }
            public string selected_icon { get; set; }
            public string un_selected_icon { get; set; }
            public string name { get; set; }
            public List<object> child_types { get; set; }
        }

        public class Medium2
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

        public class DepreciationTrend
        {
            public int? year { get; set; }
            public int? percentage { get; set; }
            public decimal? amount { get; set; }
        }

        public class Region
        {
            public int? id { get; set; }
            public string name { get; set; }
            public string currency { get; set; }
            public int? is_my_region { get; set; }
            public string flag { get; set; }
        }

        public class CarRegion
        {
            public int? price { get; set; }
            public Region region { get; set; }
        }

        public class Medium3
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
            public int? views_count { get; set; }
            public int? type { get; set; }
            public string created_at { get; set; }
            public string updated_at { get; set; }
            public int? unread_count { get; set; }
            public string name { get; set; }
            public string subtitle { get; set; }
            public object description { get; set; }
            public List<Medium3> media { get; set; }
            public List<object> child_category { get; set; }
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

        public class Review
        {
            public int? id { get; set; }
            public double average_rating { get; set; }
            public string review_message { get; set; }
            public UserDetails user_details { get; set; }
            public List<Detail> details { get; set; }
        }

        public class RegionDetail3
        {
            public int? id { get; set; }
            public string name { get; set; }
            public string currency { get; set; }
            public int? is_my_region { get; set; }
            public string flag { get; set; }
        }

        public class Details3
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
            public RegionDetail3 region_detail { get; set; }
        }

        public class ShowroomDetails3
        {
            public int? id { get; set; }
            public string name { get; set; }
            public string address { get; set; }
            public string phone { get; set; }
            public string email { get; set; }
            public string about { get; set; }
            public string logo_url { get; set; }
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
            public Details3 details { get; set; }
            public ShowroomDetails3 showroom_details { get; set; }
        }

        public class EngineType
        {
            public int? id { get; set; }
            public string name { get; set; }
        }

        public class MyCar
        {
            public int? id { get; set; }
            public int? category_id { get; set; }
            public int? version_id { get; set; }
            public double average_rating { get; set; }
            public int? views_count { get; set; }
            public int? favorite_count { get; set; }
            public int? like_count { get; set; }
            public int? personal_shopper_clicks { get; set; }
            public int? call_clicks { get; set; }
            public int? year { get; set; }
            public object chassis { get; set; }
            public object kilometer { get; set; }
            public object average_mkp { get; set; }
            public decimal? amount { get; set; }
            public string currency { get; set; }
            public string name { get; set; }
            public object email { get; set; }
            public string country_code { get; set; }
            public object phone { get; set; }
            public string notes { get; set; }
            public string life_cycle { get; set; }
            public int? status { get; set; }
            public int? is_featured { get; set; }
            public string bid_close_at { get; set; }
            public string created_at { get; set; }
            public int? is_reviewed { get; set; }
            public string transmission_type_text { get; set; }
            public string link { get; set; }
            public string status_text { get; set; }
            public string owner_type_text { get; set; }
            public List<object> top_bids { get; set; }
            public object version { get; set; }
            public bool is_liked { get; set; }
            public bool is_viewed { get; set; }
            public bool is_favorite { get; set; }
            public LimitedEditionSpecsArray limited_edition_specs_array { get; set; }
            public List<DepreciationTrendValue> depreciation_trend_value { get; set; }
            public int? review_count { get; set; }
            public string ref_num { get; set; }
            public Owner owner { get; set; }
            public CarModel car_model { get; set; }
            public CarType car_type { get; set; }
            public List<Medium2> media { get; set; }
            public List<object> my_car_attributes { get; set; }
            public RegionalSpecs regional_specs { get; set; }
            public List<DepreciationTrend> depreciation_trend { get; set; }
            public List<CarRegion> car_regions { get; set; }
            public Category category { get; set; }
            public List<Review> reviews { get; set; }
            public List<Dealer> dealers { get; set; }
            public EngineType engine_type { get; set; }
        }

        public class RegionDetail4
        {
            public int? id { get; set; }
            public string name { get; set; }
            public string currency { get; set; }
            public int? is_my_region { get; set; }
            public string flag { get; set; }
        }

        public class Details4
        {
            public int? id { get; set; }
            public string first_name { get; set; }
            public object last_name { get; set; }
            public int? is_verified { get; set; }
            public object dealer_type { get; set; }
            public object dealer_type_text { get; set; }
            public string country_code { get; set; }
            public string phone { get; set; }
            public string address { get; set; }
            public string image { get; set; }
            public bool email_updates { get; set; }
            public bool social_login { get; set; }
            public int? region_id { get; set; }
            public int? region_reminder { get; set; }
            public object limit_for_cars { get; set; }
            public object limit_for_featured_cars { get; set; }
            public object expiry_date { get; set; }
            public object about { get; set; }
            public int? gender { get; set; }
            public string nationality { get; set; }
            public object profession { get; set; }
            public string dob { get; set; }
            public string image_url { get; set; }
            public string gender_label { get; set; }
            public RegionDetail4 region_detail { get; set; }
        }

        public class Owner2
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
            public Details4 details { get; set; }
            public object showroom_details { get; set; }
        }

        public class Medium4
        {
            public int? id { get; set; }
            public int? media_type { get; set; }
            public string title { get; set; }
            public string filename { get; set; }
            public string created_at { get; set; }
            public string file_url { get; set; }
        }

        public class Brand2
        {
            public int? id { get; set; }
            public string name { get; set; }
            public List<Medium4> media { get; set; }
        }

        public class CarModel2
        {
            public int? id { get; set; }
            public string name { get; set; }
            public Brand2 brand { get; set; }
        }

        public class Medium5
        {
            public int? id { get; set; }
            public int? media_type { get; set; }
            public string title { get; set; }
            public string filename { get; set; }
            public string created_at { get; set; }
            public string file_url { get; set; }
        }

        public class MyCarAttribute
        {
            public string value { get; set; }
            public int? attr_id { get; set; }
            public string attr_name { get; set; }
            public string attr_icon { get; set; }
            public string attr_option { get; set; }
        }

        public class RegionalSpecs2
        {
            public int? id { get; set; }
            public string created_at { get; set; }
            public string name { get; set; }
        }

        public class EngineType2
        {
            public int? id { get; set; }
            public string name { get; set; }
        }

        public class TradeAgainst
        {
            public int? id { get; set; }
            public object category_id { get; set; }
            public int? version_id { get; set; }
            public double? average_rating { get; set; }
            public int? views_count { get; set; }
            public int? favorite_count { get; set; }
            public int? like_count { get; set; }
            public int? personal_shopper_clicks { get; set; }
            public int? call_clicks { get; set; }
            public int? year { get; set; }
            public string chassis { get; set; }
            public int? kilometer { get; set; }
            public object average_mkp { get; set; }
            public decimal? amount { get; set; }
            public string currency { get; set; }
            public string name { get; set; }
            public string email { get; set; }
            public string country_code { get; set; }
            public int? phone { get; set; }
            public string notes { get; set; }
            public object life_cycle { get; set; }
            public int? status { get; set; }
            public int? is_featured { get; set; }
            public string bid_close_at { get; set; }
            public string created_at { get; set; }
            public int? is_reviewed { get; set; }
            public object transmission_type_text { get; set; }
            public string link { get; set; }
            public string status_text { get; set; }
            public string owner_type_text { get; set; }
            public List<TopBid> top_bids { get; set; }
            public object version { get; set; }
            public bool is_liked { get; set; }
            public bool is_viewed { get; set; }
            public bool is_favorite { get; set; }
            public object limited_edition_specs_array { get; set; }
            public object depreciation_trend_value { get; set; }
            public int? review_count { get; set; }
            public string ref_num { get; set; }
            public Owner2 owner { get; set; }
            public CarModel2 car_model { get; set; }
            public object car_type { get; set; }
            public List<Medium5> media { get; set; }
            public List<MyCarAttribute> my_car_attributes { get; set; }
            public RegionalSpecs2 regional_specs { get; set; }
            public List<object> depreciation_trend { get; set; }
            public List<object> car_regions { get; set; }
            public object category { get; set; }
            public List<object> reviews { get; set; }
            public List<object> dealers { get; set; }
            public EngineType2 engine_type { get; set; }
        }

        public class TopBid
        {
            public int id { get; set; }
            public decimal? amount { get; set; }
            public object currency { get; set; }
            public object notes { get; set; }
            public int type { get; set; }
            public BidCloseAt2 bid_close_at { get; set; }
            public DealerInfo2 dealer_info { get; set; }
            public List<EvaluationDetail> evaluation_details { get; set; }
        }

        public class User
        {
            public int id { get; set; }
            public string name { get; set; }
            public string email { get; set; }
            public int status { get; set; }
            public string created_at { get; set; }
            public int push_notification { get; set; }
            public int cars_count { get; set; }
            public int favorite_count { get; set; }
            public int like_count { get; set; }
            public int view_count { get; set; }
            public Details3 details { get; set; }
            public ShowroomDetails showroom_details { get; set; }
        }

        public class EvaluationDetail
        {
            public decimal? amount { get; set; }
            public string currency { get; set; }
            public User user { get; set; }
        }

        public class DealerInfo2
        {
            public int id { get; set; }
            public string name { get; set; }
            public string email { get; set; }
            public int status { get; set; }
            public string created_at { get; set; }
            public int push_notification { get; set; }
            public int cars_count { get; set; }
            public int favorite_count { get; set; }
            public int like_count { get; set; }
            public int view_count { get; set; }
            public Details2 details { get; set; }
            public object showroom_details { get; set; }
        }

        public class BidCloseAt2
        {
            public string date { get; set; }
            public int timezone_type { get; set; }
            public string timezone { get; set; }
        }

        public class OfferDetail
        {
            public User user { get; set; }
            public object amount { get; set; }
            public object currency { get; set; }
        }

        public class Data
        {
            public int? id { get; set; }
            public decimal? amount { get; set; }
            public string currency { get; set; }
            public object notes { get; set; }
            public int? type { get; set; }
            public BidCloseAt bid_close_at { get; set; }            
            public List<OfferDetail> offer_details { get; set; }
            public bool is_expired { get; set; }
            public MyCar my_car { get; set; }
            public TradeAgainst trade_against { get; set; }
        }

        public class Response
        {
            public bool success { get; set; }
            public Data data { get; set; }
            public string message { get; set; }
        }
    }
}
