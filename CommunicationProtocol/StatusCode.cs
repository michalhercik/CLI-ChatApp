namespace CommunicationProtocol;

public enum StatusCode : ushort
{
    Success,
    /** 
     * User tries to send message but didn't joined any thread.
     */
    NoCurrentThread,
    /** 
     * User sends uknown command request.
     */
    UnknownCommand,
    MissingCommandParameter,
    /**
     *  User tries to create new thread but maximum number of threads is already created.
     */
    ThreadLimit,
    /** 
     * User tries to join thread but thread doesn't exists.
     */
    UknownThread,
    InvalidCommandParameter,
    InvalidPassword,
    /** 
     * User gives name of a client that doesn't exists.
     */
    UnknownClient,
    NoRights,
}
