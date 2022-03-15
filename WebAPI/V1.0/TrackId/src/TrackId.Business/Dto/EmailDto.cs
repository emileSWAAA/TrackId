using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace TrackId.Business.Dto
{
    public abstract class EmailDto
    {
        public EmailDto( IEnumerable<string> emaiAddresses)
        {
            foreach (var addr in emaiAddresses)
            {
                if (!IsValidEmail(addr))
                {
                    throw new ArgumentException($"'{addr}' is not a valid email address.");
                }
            }
        }

        public abstract string Content { get; }

        public IEnumerable<string> Addresses { get; set; }

        public abstract string Subject { get; }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address.Equals(email, StringComparison.OrdinalIgnoreCase);
            }
            catch
            {
                return false;
            }
        }
    }
}
