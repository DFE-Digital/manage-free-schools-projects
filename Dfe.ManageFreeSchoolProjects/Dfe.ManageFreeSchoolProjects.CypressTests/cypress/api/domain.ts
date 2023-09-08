export type ResponseWrapper<T> = {
    data: T;
    paging: PagingResponse;
};

export type PagingResponse = {
    recordCount: number;
};

export type BulkProjectTable<TRow> = {
    headers: Array<string>;
    rows: Array<TRow>;
};

export type BulkProjectRow = {
    projectId?: string;
    projectTitle?: string;
    trustName?: string;
    region?: string;
    localAuthority?: string;
    realisticOpeningDate?: string;
    status?: string;
};

export type CreateProjectRequest = {
    Projects: Array<ProjectDetails>;
};

export type ProjectDetails = {
    ProjectId: string;
    SchoolName: string;
    ApplicationNumber: string;
    ApplicationWave: string;
    CreatedBy: string;
};

export type CreateProjectResponse = {};
