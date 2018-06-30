using System;
using System.Collections.Generic;
using System.Text;

namespace SitecoreBlazorHosted.Shared.Models.Sitecore
{
    /// <summary>
    /// This class represents source config of <see cref="ISitecoreItem"/>.
    /// 
    /// It contains information about a database that contains the item. 
    /// For example,
    /// * master
    /// * web
    /// * core
    /// 
    /// Since the CMS serves localized content, this class also provides the language of the given item. An abbreviation of two letters is used. For example,
    /// * en
    /// * da
    /// * fr
    /// * ja
    /// * cn
    /// 
    /// As content editing is usually performed in a particular workflow, the IItemSource class stores the item's version. It is either a positive integer number or a latest version (null).
    /// 
    /// 
    /// Item source indicates the origin of a given item. It is also used in requests to define the place of a given CRUD operation. The implementation of this interface is recommended to be readonly and immutable.
    /// </summary>
    public interface IItemSource
    {
        /// <summary>
        /// Returns copy of <see cref="IItemSource"/>. It will be invoked by the session once the request is passed to it. It is done for the thread safety reasons.
        /// </summary>
        IItemSource ShallowCopy();

        /// <summary>
        /// Returns item database.
        /// For example: "web"
        /// 
        /// The value is case insensitive.
        /// </summary>
        string Database { get; }

        /// <summary>
        /// Returns item language.
        /// For example: "en"
        /// 
        /// The value is case insensitive.
        /// </summary>
        string Language { get; }

        /// <summary>
        /// Returns item version. It is a positive integer number.
        /// A null value stands for the "latest" version.
        /// 
        /// For example: 1
        /// </summary>
        int? VersionNumber { get; }
    }
}
