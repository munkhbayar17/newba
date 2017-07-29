using OLX.Connection;

namespace OLX
{
    class Program
    {
        static void Main(string[] args)
        {
            // begin user test
            Processor processer = new Processor();
            // save all user_id in memeory
            processer.Save_Users_in_Memory();
            // run user_messages_test
            processer.Select_User_Message_Text();
        }
    }
}
