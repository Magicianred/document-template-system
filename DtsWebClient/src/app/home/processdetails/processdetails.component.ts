import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { DetailedProcess } from '../home.component';


@Component({
  selector: 'app-processdetails',
  templateUrl: './processdetails.component.html',
  styleUrls: ['./processdetails.component.css']
})
export class ProcessDetailsComponent implements OnInit {

  @Input()
  specificProcess: DetailedProcess;

  @Input()
  activeComment: string;

  @Output() sendComment: EventEmitter<string> = new EventEmitter<string>()

  @Output() sendId: EventEmitter<string> = new EventEmitter<string>()
  
  constructor() { }

  sendIdBack() {
    this.sendId.emit(this.specificProcess.id.toString())
  }

  sendCommentBack(comment: string) {
    document.getElementById("inputField").value = "";
    this.sendComment.emit(comment)
  }

  ngOnInit() {
  }

}
