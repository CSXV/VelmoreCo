using DataAccessLayer;

namespace BusinessLayer;

public class bl_Users
{
    public enum enMode
    {
        addNew = 0,
        Update = 1,
    };

    public enMode Mode = enMode.addNew;

    public UserDTO UDTO
    {
        get
        {
            return (
                new UserDTO(
                    this.ID,
                    this.firstName,
                    this.lastName,
                    this.email,
                    this.password,
                    this.createDate,
                    this.updateDate,
                    this.type
                )
            );
        }
    }

    public int ID { get; set; }
    public string firstName { get; set; }
    public string lastName { get; set; }
    public string email { get; set; }
    public string password { get; set; }
    public DateTime createDate { get; set; }
    public DateTime updateDate { get; set; }
    public int type { get; set; }

    public bl_Users(UserDTO UDTO, enMode cMode = enMode.addNew)
    {
        this.ID = UDTO.ID;
        this.firstName = UDTO.firstName;
        this.lastName = UDTO.lastName;
        this.email = UDTO.email;
        this.password = UDTO.password;
        this.createDate = UDTO.createDate;
        this.updateDate = UDTO.updateDate;
        this.type = UDTO.type;

        this.Mode = cMode;
    }

    public static bl_Users login(string email, string password)
    {
        UserDTO UDTO = da_UsersData.loginUser(email, password);

        if (UDTO != null)
        {
            return new bl_Users(UDTO, enMode.Update);
        }
        else
        {
            return null;
        }
    }

    public static bl_Users Find(int id)
    {
        UserDTO UDTO = da_UsersData.GetUserByID(id);

        if (UDTO != null)
        {
            return new bl_Users(UDTO, enMode.Update);
        }
        else
        {
            return null;
        }
    }

    private bool _AddNewUser()
    {
        this.ID = da_UsersData.AddNewUser(UDTO);
        return (this.ID != -1);
    }

    private bool _UpdateUserByID()
    {
        return da_UsersData.UpdateUserByID(UDTO);
    }

    public static bool DeleteUser(int id)
    {
        return da_UsersData.DeleteUserByID(id);
    }

    public bool Save()
    {
        switch (Mode)
        {
            case enMode.addNew:
                if (_AddNewUser())
                {
                    Mode = enMode.Update;
                    return true;
                }
                else
                {
                    return false;
                }
            case enMode.Update:
                return _UpdateUserByID();
        }

        return false;
    }
}
