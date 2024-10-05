import { Component, OnInit  } from '@angular/core';
import { ToolbarModule } from 'primeng/toolbar';
import { ButtonModule } from 'primeng/button';
import { TableModule } from 'primeng/table';
import { ApiPedidoService } from '../apipedido.service'; 

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [ToolbarModule, ButtonModule, TableModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {
  pedidos: any[] = [];

  constructor(private apiPedidoService: ApiPedidoService) {}

  ngOnInit() {
    this.cargarPedidos(); 
  }

  cargarPedidos() {
    this.apiPedidoService.getPedidos().subscribe({
      next: (response) => {
        if (response && response.response && Array.isArray(response.response)) {
          this.pedidos = response.response; // Asignando del array de pedidos
          console.log('Pedidos cargados:', this.pedidos); // Imprime los pedidos
        } else {
          console.error('La respuesta no contiene un array de pedidos', response);
        }
      },
      error: (error) => {
        console.error('Error al cargar los pedidos', error);
      }
    });
  }

}
