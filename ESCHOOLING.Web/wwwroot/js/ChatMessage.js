class ChatMessage {
    constructor(id, fromUserId, toUserId, message, timeSent, hasRead) {
        this.Id = id;
        this.FromUserId = fromUserId;
        this.ToUserId = toUserId;
        this.Message = message;
        this.TimeSent = timeSent;
        this.hasRead = hasRead;

    }
}