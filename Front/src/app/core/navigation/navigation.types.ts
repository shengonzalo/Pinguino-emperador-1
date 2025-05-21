import { OgaNavigationItem } from '@oga/components/navigation';

export interface Navigation {
    compact: OgaNavigationItem[];
    default: OgaNavigationItem[];
    futuristic: OgaNavigationItem[];
    horizontal: OgaNavigationItem[];
}

export interface NavigationApiResponse {
    isLiteral?: boolean;
    literal?: null | string;
    menuItems: Navigation[];
    id: number;
    name: string;
    description: string;
    icon: string;
    rootId: number;
    parentId: number | null;
    path: null | string;
    order: number;
    elementId: string;
}
