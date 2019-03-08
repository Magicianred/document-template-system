import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { Process } from '../home.component';

@Component({
  selector: 'app-processes',
  templateUrl: './processes.component.html',
  styleUrls: ['./processes.component.css']
})
export class ProcessesComponent implements OnInit {

  @Input()
  processes: Process[];

  @Output() send: EventEmitter<string> = new EventEmitter<string>()


  constructor() { }

  sendIdBack(id: string) {
    
    this.send.emit(id)
  }

  ngOnInit() {
  }

}
