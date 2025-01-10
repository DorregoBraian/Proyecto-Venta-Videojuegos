import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-alert-personalizados',
  standalone: true,
  imports: [],
  templateUrl: './alert-personalizados.component.html',
  styleUrl: './alert-personalizados.component.css'
})
export class AlertPersonalizadosComponent {

  constructor(
    public dialogRef: MatDialogRef<AlertPersonalizadosComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { title: string; message: string; imageUrl: string }
  ) {}

  closeDialog(): void {
    this.dialogRef.close();
  }
}
