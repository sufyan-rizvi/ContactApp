using System.Security.Authentication;
using ContactApp.Exceptions;
using ContactApp.Models;

namespace ContactApp.Repository
{
    internal class ContactDetailRepository
    {
        public static Contact CurrentContact { get; set; }

        public static List<ContactDetail> CurrentContactDetails { get; set; }
        //public static List<ContactDetail> ContactDetails { get; set; }


        public ContactDetailRepository(Contact contact)
        {
            CurrentContact = contact;
            CurrentContactDetails = CurrentContact.ContactDetails;
            //CurrentContactDetails = ContactDetails.Where(d => d.ContactId == CurrentContact.ContactId).ToList();

        }


        public static List<ContactDetail> ViewAllContactDetails()
        {
            AnyDetailsInList();
            return CurrentContactDetails;
        }

        public static void AddContactDetail(ContactDetail detail)
        {
            if (CurrentContactDetails.Count == 0)
                detail.ContactDetailId = 1;
            else
                detail.ContactDetailId = CurrentContactDetails.Last().ContactDetailId + 1;

            detail.ContactId = CurrentContact.ContactId;
            CurrentContactDetails.Add(detail);
            CurrentContactDetails.Add(detail);
        }

        public static void DeleteDetail(int id)
        {
            ContactDetail detail = GetById(id);
            CurrentContactDetails.Remove(detail);
            //ContactDetails.Remove(detail);
        }

        public static ContactDetail GetById(int id)
        {
            ContactDetail detail = CurrentContactDetails.Where(details => details.ContactDetailId == id).FirstOrDefault()!;
            if (detail == null)
                throw new NoElementFoundException("No such contact detail found !");
            return detail;
        }

        static void AnyDetailsInList()
        {
            if (CurrentContactDetails.Count == 0)
                throw new ListEmptyException("No Details for the current contact");
        }

        public static void UpdateDetail(ContactDetail detail)
        {
            UpdatePositionInList(detail.ContactDetailId, CurrentContactDetails, detail);
            //UpdatePositionInList(detail.ContactDetailId, ContactDetails, detail);
            
        }

        public static void UpdatePositionInList(int id, List<ContactDetail> details, ContactDetail detail)
        {
            for (int i = 0; i < details.Count; i++)
            {
                if (details[i].ContactDetailId == id)
                {
                    details[i] = detail;
                    return;
                }
            }
            throw new NoElementFoundException("No such Detail Exists !");



        }
    }
}
