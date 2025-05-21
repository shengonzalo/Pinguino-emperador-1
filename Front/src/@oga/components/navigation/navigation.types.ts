import {
    IsActiveMatchOptions,
    Params,
    QueryParamsHandling,
} from '@angular/router';

export interface OgaNavigationItem {
    id?: string;
    isLiteral?: boolean;
    title?: string;
    subtitle?: string;
    type: 'aside' | 'basic' | 'collapsable' | 'divider' | 'group' | 'spacer';
    hidden?: (item: OgaNavigationItem) => boolean;
    active?: boolean;
    disabled?: boolean;
    tooltip?: string;
    link?: string;
    fragment?: string;
    preserveFragment?: boolean;
    queryParams?: Params | null;
    queryParamsHandling?: QueryParamsHandling | null;
    externalLink?: boolean;
    target?: '_blank' | '_self' | '_parent' | '_top' | string;
    exactMatch?: boolean;
    isActiveMatchOptions?: IsActiveMatchOptions;
    function?: (item: OgaNavigationItem) => void;
    classes?: {
        title?: string;
        subtitle?: string;
        icon?: string;
        wrapper?: string;
    };
    icon?: string;
    badge?: {
        title?: string;
        classes?: string;
    };
    children?: OgaNavigationItem[];
    meta?: any;
}

export type OgaVerticalNavigationAppearance =
    | 'default'
    | 'compact'
    | 'dense'
    | 'thin';

export type OgaVerticalNavigationMode = 'over' | 'side';

export type OgaVerticalNavigationPosition = 'left' | 'right';
