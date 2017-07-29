using System;
using System.Collections.Generic;

namespace OLX.Recommender
{
    class Recommender
    {
        public static int[] recommend_for_existing_user(int user_id, int category_id) {
            return new int[3] { 1, 2, 3 };
        }

        public static int[] recommend_for_new_user(int user_id, int category_id)
        {
            return new int[3] { 4, 5, 6 };
        }

        // NON USER RECOMMENDATION
        // most messaged and viewed
        static void get_most_messaged_ads_recent_week() { }
        static void get_most_viewed_ads_recent_week() { }
        static void get_cheaper_of_most_messaged_ads_recent_week() { }
        static void get_cheaper_of_most_viewed_ads_recent_week() { }
        // reliable seller products, reliable == most successful seller
        static void get_recent_products_of_reliable_sellers() { }


        // GET CHEAPER is cheaper than average in the category
        static void get_cheaper() { }

        static void get_similar_products(int category, string title, string desc) { }
    }
}
