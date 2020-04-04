import { Component, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { Record } from '../../models/record.model';
import { RecordService } from '../../services/record.service';
import { ImageService } from '../../services/image.service';

@Component({
  selector: 'app-record-editor',
  templateUrl: './record-editor.component.html',
  styleUrls: ['./record-editor.component.scss']
})
export class RecordEditorComponent implements OnInit {
  model: Record = {
    date: new Date(),
    title: '',
    imageUrl: ''
  };

  selectedFile: File

  constructor(private recordService: RecordService,
    private imageService: ImageService,
    private dialogRef: MatDialogRef<RecordEditorComponent>) { }

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

  onSubmit(data: Record) {
    console.log(data);
    this.recordService.insertOrReplaceRecord(data).toPromise()
      .then((_) => this.dialogRef.close());
  }

  onCancel() {
    this.dialogRef.close();
  }
}
