namespace UserLoginCRUDWebApp.Models
{
    public class UserComment
    {
        //A class/object is going to have properties:
        //Data model declaration, main entity
        public int Id { get; set; }
        public String CommentDescription { get; set; }

        public String Details { get; set; }

        //Method for a constructor
        public UserComment()
        {
               //Will be used by other pieces of the app code
        }

    }
}
