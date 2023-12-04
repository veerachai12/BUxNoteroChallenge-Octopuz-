using DataStore.Quiz;
using Notero.Unity.Networking.Mirror;

namespace BU.RRTT.QuizExampleMessages.EventHandler
{
    public class StudentQuizEventHandler : IEventHandler
    {
        private QuizStore m_Store;

        public StudentQuizEventHandler(QuizStore store)
        {
            m_Store = store;
        }

        public void Subscribe(IEventSubscribable subscribable)
        {
            subscribable.Subscribe<CurrentQuestionMessage>(OnCurrentQuestionReceived);
            subscribable.Subscribe<QuizResultMessage>(OnQuizResultReceived);
            subscribable.Subscribe<QuizInfoMessage>(OnQuizInfoReceived);
            subscribable.Subscribe<AnswerCorrectMessage>(OnAnswerCorrectMessage);
            subscribable.Subscribe<PreTestResultMessage>(OnPreTestResultReceived);
            subscribable.Subscribe<CustomDataMessage>(OnCustomDataMessageReceived);
        }

        public void Unsubscribe(IEventSubscribable subscribable)
        {
            subscribable.Unsubscribe<CurrentQuestionMessage>(OnCurrentQuestionReceived);
            subscribable.Unsubscribe<QuizResultMessage>(OnQuizResultReceived);
            subscribable.Unsubscribe<QuizInfoMessage>(OnQuizInfoReceived);
            subscribable.Unsubscribe<AnswerCorrectMessage>(OnAnswerCorrectMessage);
            subscribable.Unsubscribe<PreTestResultMessage>(OnPreTestResultReceived);
            subscribable.Unsubscribe<CustomDataMessage>(OnCustomDataMessageReceived);
        }

        private void OnQuizResultReceived(QuizResultMessage message)
        {
            m_Store.SetQuizResult(message.CorrectAnswerAmount);
        }

        private void OnPreTestResultReceived(PreTestResultMessage message)
        {
            m_Store.SetPreTestResult(new PreTestResult { HasScore = message.HasScore, Score = message.Score, FullScore = message.FullScore });
        }

        private void OnQuizInfoReceived(QuizInfoMessage message)
        {
            m_Store.SetQuizInfo(new QuizInfo(
                currentQuizNumber: message.CurrentQuizNumber,
                questionAmount: message.QuestionAmount
            ));
        }

        private void OnAnswerCorrectMessage(AnswerCorrectMessage message)
        {
            m_Store.SetCorrectAnswer(message.Answer);
        }

        private void OnCustomDataMessageReceived(CustomDataMessage message)
        {
            m_Store.SetCustomData(message.Data);
        }

        private void OnCurrentQuestionReceived(CurrentQuestionMessage message)
        {
            m_Store.SetCurrentQuestion(new Question
            {
                Id = message.Id,
                AssetFile = message.AssetFile,
                Answer = new Answer { AssetAnswerFile = message.AssetAnswerFile },
                QuestionAssetType = message.QuestionAssetType
            });
        }
    }
}