import { Component, OnInit } from '@angular/core';
import { ResultService } from '../../services/result.service';
import { AppError } from '../../common/app-error';
import { NotFoundError } from '../../common/not-found-error';
import { BadInput } from '../../common/bad-input';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-result',
  templateUrl: './result.component.html',
  styleUrls: ['./result.component.css']
})

export class ResultComponent implements OnInit {
  form = new FormGroup({
    'participant': new FormControl('', Validators.required),
    'discipline': new FormControl('', Validators.required),
    'disciplineResult': new FormControl('', Validators.required)
  });

  get ParticipantName() { return this.form.get('name'); }


  results: Result[];

  constructor(private service: ResultService) { //separation of concerns, use service -> not directly use http here...
    // no call of http services in constructor!
    ;
  }

  ngOnInit() {
    this.service.getAll()
      .subscribe(response => {
        this.results = response;
        //error handled by AppErrorHandler
      });
  }

  insertResult() {
    let result: Result = {
      ResultId: 0, //to do, insert
      ParticipantId: this.form.value.participant, //to do, insert
      DisciplineId: this.form.value.discipline,
      DisciplineResult: this.form.value.disciplineResult,
      DisciplineScore: -1
    }; //assign value to local variable
    this.form.reset();
    //optimistic update already here, will be withdrawn in case of error
    this.results.splice(0, 0, result);

    //participant.value = ''; //delete input after assessing value

    this.service.create(result)
      .subscribe(
        response => {
          result['Id'] = response;

          // pessimistic update here: this.participants.splice(0, 0, participant);
        },
        (error: AppError) => {
          // withdraw optimistic update in case of error
          this.results.splice(0, 1);

          if (error instanceof BadInput) {
            //this.form.setErrors(error.originalError)
          }
          else throw error; //hit global error handler
          /*throw error replaces: {
            //alert function not best, since window will be frozen... -> save log to db
            alert('An unexpected error occurred.');
          }*/
        }
      );
  }

  updateResult(result: Result) {
    this.service.update(result, result.ResultId)
      .subscribe(
        response => {
          console.log(response);
        });
  }

  deleteResult(result: Result) {
    // optimistic update 
    let index = this.results.indexOf(result);
    this.results.splice(index, 1);

    this.service.delete(result.ResultId)
      .subscribe(
        response => {
        },
        (error: AppError) => {
          this.results.splice(index, 0, result);

          if (error instanceof NotFoundError)
            alert('This result has already been deleted')
          else throw error;
        });
  }

}


interface Participant {
  ParticipantId: number,
  PreName: string;
  Name: string;
  YearOfBirth: number;
  Club: string;
  Cathegory: string;
}


interface Result {
  ResultId: number,
  ParticipantId: number,
  DisciplineId: number;
  DisciplineResult: number;
  DisciplineScore: number;
}
