import{PullReq} from "./pull-req";
//import{SignalR} from "signalr/node";

export interface FeedSignalR extends SignalR {
    broadcaster: FeedProxy
}
 
export interface FeedProxy {
    client: FeedClient;
}
 
export interface FeedClient {
    setConnectionId: (id: string) => void;
    updatePullRequests: (pullRequests: PullReq[]) => void;
}
 
export enum SignalRConnectionStatus {
    Connected = 1,
    Disconnected = 2,
    Error = 3
}