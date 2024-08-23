using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ContactApp.Exceptions;
using ContactApp.Models;
using ContactApp.Services;

namespace ContactApp.Repository
{
    internal class UserRepository
    {
        public static List<User> Users { get; set; } 

        public static User CurrentUser { get; set; }

        public UserRepository(User user)
        {
            CurrentUser = user;
        }


        public static List<User> ViewAllUsers()
        {
            AnyUsersInList();
            return Users;
        }

        public static void AddUser(User user)
        {
            user.UserId = Users.Last().UserId + 1;
            Users.Add(user);
        }

        public static void UpdateUser(User user, int position)
        {
            Users[position] = user;
        }  
        
        public static int GetPositionInList(int id)
        {
            for(int i = 0; i < Users.Count; i++)
            {
                if (Users[i].UserId == id)
                    return i;
            }
            throw new NoElementFoundException("No such User Exists !");
        }

        public static void SetInactive(int id)
        {
            User user = GetById(id);
            (var adminCount, var staffCount) = CountAdminAndStaff();
            if (user.IsAdmin && adminCount == 1)
                throw new CannotDeleteSelfException("You are the only Admin ! Cannot delete Self !");
            if (!user.IsAdmin && staffCount == 1)
                throw new NotEnoughUsersException("There are not enough staff Members! Cannot Delete this user !");                 
            if (id == CurrentUser.UserId)
                throw new CannotDeleteSelfException("Cannot Delete Self");            
            user.IsActive = false;
        }

        public static void SetAdmin(int id)
        {
            User user = GetById(id);
            (var adminCount, var staffCount) = CountAdminAndStaff();
            if (!user.IsAdmin && staffCount == 1)
                throw new NotEnoughUsersException("Not enough Staff Members ! Cannot make this user Admin !");            
            CheckActive(user);
            user.IsAdmin = true;
        }

        public static User GetById(int id)
        {
            User user = Users.Where(user => user.UserId == id).FirstOrDefault()!;
            if (user != null)
                return user;
            throw new NoElementFoundException("No user found !");
        }

        public static void CheckAdmin(User user)
        {
            if (!user.IsAdmin)
                throw new NotAdminException("The User is not an Admin");
        }

        public static void CheckActive(User user)
        {
            if (!user.IsActive)
                throw new NotActiveException("The user is not Active");
        }

        public static void AnyUsersInList()
        {
            if (Users.Count == 0)
                throw new ListEmptyException("No Users in the List");
        }

        public static (int,int) CountAdminAndStaff()
        {
            int countAdmin = 0;
            int countStaff = 0;
            foreach (var user in Users)
            {
                if(user.IsAdmin && user.IsActive)
                    countAdmin++;
                if (!user.IsAdmin && user.IsActive)
                    countStaff++;
            }     
            return (countAdmin, countStaff);
            
        }

        public static void UpdateContactsOfUser(List<Contact> contacts, int userId)
        {
            for (int i = 0; i < Users.Count; i++)
            {
                if (Users[i].UserId == userId)
                {
                    Users[i].Contacts = contacts;
                    return;
                }
                    

            }

        }



    }
}
