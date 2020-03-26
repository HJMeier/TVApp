import { Component, OnInit } from '@angular/core';
import { ParticipantService } from '../../services/participant.service';
import { AppError } from '../../common/app-error';
import { NotFoundError } from '../../common/not-found-error';
import { BadInput } from '../../common/bad-input';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-participant',
  templateUrl: './participant.component.html',
  styleUrls: ['./participant.component.css']
})
export class ParticipantComponent implements OnInit {
  form = new FormGroup({
    'name': new FormControl('', Validators.required),
    'preName': new FormControl('', Validators.required),
    'yearOfBirth': new FormControl('', Validators.required),
    'club': new FormControl('', Validators.required),
    'cathegory': new FormControl('', Validators.required)
  });

  get ParticipantName() { return this.form.get('name'); }


  participants: Participant[];
  //participants: any;

  constructor(private service: ParticipantService) { //separation of concerns, use service -> not directly use http here...
    // no call of http services in constructor!
    ;
  }

  ngOnInit() {
    this.service.getAll()
      .subscribe(response => {
        this.participants = response;
        //error handled by AppErrorHandler
      });
  }

  createParticipant() {
    let participant: Participant = {
      ParticipantId: 0,
      PreName: this.form.value.preName,
      Name: this.form.value.name,
      YearOfBirth: this.form.value.yearOfBirth,
      Club: this.form.value.club,
      Cathegory: this.form.value.cathegory
    }; //assign value to local varibale
    this.form.reset();
    //optimistic update already here, will be withdrawn in case of error
    this.participants.splice(0, 0, participant);

    //participant.value = ''; //delete input after assessing value

    this.service.create(participant)
      .subscribe(
        response => {
          participant['Id'] = response;

          // pessimistic update here: this.participants.splice(0, 0, participant);
        },
        (error: AppError) => {
          // withdraw optimistic update in case of error
          this.participants.splice(0, 1);

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

  updateParticipant(participant: Participant) {
    this.service.update(participant, participant.ParticipantId)
      .subscribe(
        response => {
          console.log(response);
        });
  }

  deleteParticipant(participant: Participant) {
    // optimistic update 
    let index = this.participants.indexOf(participant);
    this.participants.splice(index, 1);

    this.service.delete(participant.ParticipantId)
      .subscribe(
        response => {
        },
        (error: AppError) => {
          this.participants.splice(index, 0, participant);

          if (error instanceof NotFoundError)
            alert('This participant has already been deleted')
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
