using DataAccessLayer;

namespace BusinessLayer;

public class bl_Reviews
{
    public enum enMode
    {
        addNew = 0,
        Update = 1,
    };

    public enMode Mode = enMode.addNew;

    public ReviewDTO RDTO
    {
        get
        {
            return (
                new ReviewDTO(
                    this.ID,
                    this.ProductID,
                    this.UserID,
                    this.rating,
                    this.comment,
                    this.createDate
                )
            );
        }
    }

    public AllReviewDTO ARDTO
    {
        get
        {
            return (
                new AllReviewDTO(
                    this.ID,
                    this.ProductID,
                    this.UserID,
                    this.rating,
                    this.comment,
                    this.createDate,
                    this.firstName,
                    this.lastName
                )
            );
        }
    }

    public int ID { get; set; }
    public int ProductID { get; set; }
    public int UserID { get; set; }
    public int rating { get; set; }
    public string comment { get; set; }
    public DateTime createDate { get; set; }
    public string firstName { get; set; }
    public string lastName { get; set; }

    public bl_Reviews(ReviewDTO RDTO, enMode cMode = enMode.addNew)
    {
        this.ID = RDTO.ID;
        this.ProductID = RDTO.ProductID;
        this.UserID = RDTO.UserID;
        this.rating = RDTO.rating;
        this.comment = RDTO.comment;
        this.createDate = RDTO.createDate;

        this.Mode = cMode;
    }

    public static List<AllReviewDTO> GetAllReviewsForProductID(int id)
    {
        return da_ReviewData.GetAllReviewsForProductID(id);
    }

    public static bl_Reviews Find(int id)
    {
        ReviewDTO RDTO = da_ReviewData.GetReviewByID(id);

        if (RDTO != null)
        {
            return new bl_Reviews(RDTO, enMode.Update);
        }
        else
        {
            return null;
        }
    }

    private bool _AddNewReview()
    {
        this.ID = da_ReviewData.AddNewReview(RDTO);
        return (this.ID != -1);
    }

    private bool _UpdateReviewByID()
    {
        return da_ReviewData.UpdateReviewByID(RDTO);
    }

    public static bool DeleteReview(int id)
    {
        return da_ReviewData.DeleteReviewByID(id);
    }

    public bool Save()
    {
        switch (Mode)
        {
            case enMode.addNew:
                if (_AddNewReview())
                {
                    Mode = enMode.Update;
                    return true;
                }
                else
                {
                    return false;
                }
            case enMode.Update:
                return _UpdateReviewByID();
        }

        return false;
    }
}
