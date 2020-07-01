using System;

namespace ESGI.DesignPattern.Projet
{
    public enum SubjectUserConfirmation
    {
        TermsAndConditions,
        NewsLetterSubscription
    }

    public class UserConfirmationFactory
    {
        public static IUserConfirmation Create(SubjectUserConfirmation subjectUserConfirmation, Product product, IMessageBoxWrapper messageBoxWrapper)
        {
            switch (subjectUserConfirmation)
            {
                case SubjectUserConfirmation.TermsAndConditions: return new TermsAndConditionsConfirmation(product, messageBoxWrapper);
                case SubjectUserConfirmation.NewsLetterSubscription: return new NewsletterConfirmation(product, messageBoxWrapper);
                default: throw new NotImplementedException("Subject of confirmation not implemented");
            }
        }
    }
}
