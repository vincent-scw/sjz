import { Component, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Moment } from '../../../models/moment.model';
import { MomentService } from '../../../services/moment.service';
import { ImageService } from '../../../services/image.service';

@Component({
  selector: 'app-moment-editor',
  templateUrl: './moment-editor.component.html',
  styleUrls: ['./moment-editor.component.scss']
})
export class MomentEditorComponent implements OnInit {
  model: Moment = {
    date: new Date(),
    title: '',
    imageUrl: ''
  };

  selectedFile: File;

  constructor(private recordService: MomentService,
    private imageService: ImageService,
    private dialogRef: MatDialogRef<MomentEditorComponent>) { }

  ngOnInit() {
    
  }

  onFileChanged(event) {
    this.selectedFile = event.target.files[0]
  }

  onUploadClicked() {
    this.imageService.uploadImage(this.selectedFile)
      .subscribe(ret => {
        this.model.imageUrl = ret.url
        this.model.thumbnailUrl = ret.thumbnail
      },
        error => {
          alert('Failed!');
          console.error(error);
        });
  }

  onSubmit(data: Moment) {
    this.recordService.insertOrReplaceRecord(data).toPromise()
      .then((_) => this.dialogRef.close());
  }

  onCancel() {
    this.dialogRef.close();
  }
}
