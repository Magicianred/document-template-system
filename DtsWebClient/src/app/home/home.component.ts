import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';



@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  apiClient: HttpClient;
  baseUrl: string;
  processId: string;
  activeComment: string;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.apiClient = http;
    this.baseUrl = baseUrl;
  }

  ngOnInit() {
  }
}
