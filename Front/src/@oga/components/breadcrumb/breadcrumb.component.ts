import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { MatIcon, MatIconModule } from '@angular/material/icon';
import { TranslocoModule } from '@ngneat/transloco';
import { BreadcrumbItem } from 'app/core/types/breadcrumbItem.types';

@Component({
    selector: 'base-breadcrumb',
    templateUrl: './breadcrumb.component.html',
    styleUrls: ['./breadcrumb.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
    standalone: true,
    imports: [TranslocoModule, CommonModule, MatIconModule],
})
export class BreadcrumbComponent {
    @Input() breadcrumbItems: BreadcrumbItem[] = [];
}
