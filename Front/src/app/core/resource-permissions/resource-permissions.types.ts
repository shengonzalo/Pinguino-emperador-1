export interface ResourcePermissions {
    resource: Resource;
    permissions: Permission[];
    nodes: ResourcePermissions[];
}

export interface Permission {
    id: number;
    name: string;
    description: string;
    allowed: boolean;
    resourceId: number;
    resourceName: string;
    permissionType: PermissionType;
}

export interface PermissionType {
    id: number;
    description: string;
}

export interface Resource {
    id: number;
    name: string;
    description: string;
}
