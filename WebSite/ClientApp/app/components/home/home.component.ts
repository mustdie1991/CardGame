import { Component, OnInit } from '@angular/core';
import { HubConnection } from '@aspnet/signalr-client';

@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})
export class HomeComponent implements OnInit {
    title = 'app';
    private _hubConnection: HubConnection;
    message = '';
    messages: string[] = [];

    constructor() { }

    public sendMessage(): void {
        const data = `Sent : ${this.message}`;
        this._hubConnection.invoke('Send', data);
        this.messages.push(data);
    }

    ngOnInit() {
        this._hubConnection = new HubConnection('/chat');

        this._hubConnection.on('Send', (data: any) => {
            const received = `Received: ${data}`;
            this.messages.push(received);
        })

        this._hubConnection.start().then(() => {
            console.log('Hub started!')
        }).catch(err => {
            console.log('Error while establishing connection');
        });
    }
}
