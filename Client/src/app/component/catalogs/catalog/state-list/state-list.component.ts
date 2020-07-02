import { Component, OnInit } from '@angular/core';
import { State } from '../../../../models/state';
import { RepositoryService } from '../../../../services/repository.service';
import { NzNotificationService } from '../../../../../../node_modules/ng-zorro-antd';

@Component({
  selector: 'app-state-list',
  templateUrl: './state-list.component.html',
  styleUrls: ['./state-list.component.css']
})
export class StateListComponent implements OnInit {

  states: Array<State> = new Array<State>();
  private stateUrl = 'states';

    constructor(
        private repo: RepositoryService,
        private notification: NzNotificationService
    ) { }

    ngOnInit() {
        this.loadData();
    }

    loadData() {
      this.repo.get(this.stateUrl).subscribe(data => {this.states = data as Array<State>; });
    }
    onAdd(state: State) {
      this.repo.post(this.stateUrl, state).subscribe(() => {
          this.loadData();
          this.notification.success('Catálogo', 'Se ha adicionado el catálogo satisfactoriamente');
      });
  }

  onEdit(state: State) {
    this.repo.update(`${this.stateUrl}/${state.id}`, state).subscribe(() => {
      this.loadData();
      this.notification.success('Catálogo', 'Se ha editado el catálogo satisfactoriamente');
  });
  }

  onDelete(state: State) {
    this.repo.delete(`${this.stateUrl}/${state.id}`).subscribe(() => {
      this.loadData();
      this.notification.success('Catálogo', 'Se ha eliminado el catálogo satisfactoriamente');
  });
  }

}
