namespace RabbitReview.Repositories
{
    public interface IQueueConsumer
    {
        public void ReadItem(byte[] body);
    }
}
