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
