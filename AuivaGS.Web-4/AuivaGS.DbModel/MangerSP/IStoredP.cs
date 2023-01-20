﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using AuivaGS.DbModel.ModelSP;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace AuivaGS.DbModel.MangerSP
{
    public partial interface IStoredP
    {
        Task<List<DeleteCommentIDResult>> DeleteCommentIDAsync(int? UserID, int? CommentID, int? ForumID, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<DeletePostForumResult>> DeletePostForumAsync(int? UserID, int? ForumID, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<AddCommitForumResult>> AddCommitForumAsync(int? UserID, int? ForumID, string CommitDescriptionForum, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<GetAllForumResult>> GetAllForumAsync(int? UserID, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<GetCommentToPostResult>> GetCommentToPostAsync(int? UserID, int? ForumID, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<GetForumIDUserResult>> GetForumIDUserAsync(int? UserID, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<GetForumIsIDResult>> GetForumIsIDAsync(int? UserID, int? ForumID, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<PostFourmResult>> PostFourmAsync(int? UserID, string TitleForum, string DescriptionForum, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
    }
}
