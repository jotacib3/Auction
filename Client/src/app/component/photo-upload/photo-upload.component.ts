import { Component, OnInit, ElementRef, ViewChild, Output, Input, EventEmitter } from '@angular/core';
import { UploadFile, NzMessageService } from '../../../../node_modules/ng-zorro-antd';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-photo-upload',
  templateUrl: './photo-upload.component.html',
  styleUrls: ['./photo-upload.component.css']
})
export class PhotoUploadComponent implements OnInit {

  showUploadList = {
    showPreviewIcon: true,
    showRemoveIcon: true,
    hidePreviewIconInNonImage: true
  };
  @ViewChild('fileInput') fileInput: ElementRef;
  previewImage: string | undefined = '';
  previewVisible = false;
  loading = false;
  avatarUrl: string;
  uploading = false;
  @Output() upload = new EventEmitter<any>();
  fileList: UploadFile[] = [];
  file: File;
  constructor(private userService: UserService, private msg: NzMessageService) { }

  beforeUpload = (file: File): boolean => {
   this.upload.emit(file);
    return true;
  }

  handlePreview = (file: UploadFile) => {
    this.previewImage = file.url || file.thumbUrl;
    this.previewVisible = true;
  }

  handleUpload(): void {
    const formData = new FormData();
    // formData.append('file', this.fileList1[0].originFileObj);

    this.uploading = true;

    this.userService.postPhoto('photo/employee/d1518e5e-b1ff-40e0-b708-dd4f09bb6763', formData).subscribe(data => {
      this.uploading = false;
      // this.fileList = [];
      this.msg.success('Upload Successfully'); },
      error => {
        this.msg.error(error);
    });
  }

  UploadPhoto(): void {
    const nativeElement: HTMLInputElement = this.fileInput.nativeElement;
    const formData: FormData = new FormData();
    const file: File = nativeElement.files[0];
    console.log(nativeElement.files[0]);
    formData.append('file', file, file.name );

    this.uploading = true;

    this.userService.postPhoto('photo/employee/d1518e5e-b1ff-40e0-b708-dd4f09bb6763', file).subscribe(data => {
      this.uploading = false;
      // this.fileList = [];
      this.msg.success('Upload Successfully'); },
      error => {
        this.msg.error(error);
    });
  }

  handleChange(): void {
    this.file =  this.fileList[0].originFileObj;
    const formData = new FormData();
    formData.append('file', this.file);
    // this.uploading = true;

    // this.userService.postPassword('photo/employee/d1518e5e-b1ff-40e0-b708-dd4f09bb6763', fil).subscribe(data => {
    //   this.uploading = false;
    //   this.fileList = [];
    //   this.msg.success('Upload Successfully'); },
    //   error => {
    //     this.msg.error(error);
    // });
  }

  ngOnInit() {
  }

}
