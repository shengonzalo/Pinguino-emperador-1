export interface Permission {
    id: number;
    name: string;
    description: string;
    order: number;
    rolePermissionGroup: RolePermissionGroup[];
}

export interface RolePermissionGroup {
    groupId: number;
    roleId: number;
    roleName: string;
    roleDesc: string;
}
