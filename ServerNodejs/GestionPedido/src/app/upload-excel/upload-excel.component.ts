import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-upload-excel',
  standalone: true,
  imports: [],
  templateUrl: './upload-excel.component.html',
  styleUrl: './upload-excel.component.css'
})

export class UploadExcelComponent {

  selectedFile: File | null = null;

  constructor(private http: HttpClient) {}

  onFileSelected(event: any): void {
    this.selectedFile = event.target.files[0];
  }

  onUpload(): void {
    if (this.selectedFile) {
      const formData = new FormData();
      formData.append('file', this.selectedFile, this.selectedFile.name);
  
      // Obtener el token del localStorage
      const token = localStorage.getItem('token');
  
      this.http.post('http://localhost:5032/api/Upload/uploadexcel', formData, {
        headers: {
          'Authorization': `Bearer ${token}`
        }
      })
      .subscribe({
        next: (response) => {
          console.log('Carga exitosa:', response);
        },
        error: (error) => {
          console.error('Error al cargar el archivo:', error);
        }
      });
    }
  }

}
