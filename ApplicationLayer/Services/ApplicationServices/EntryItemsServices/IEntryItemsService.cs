using System;
using ApplicationLayer.DTOs;
using System.Collections.Generic;
using ApplicationLayer.Services.ApplicationServices.BaseServices;


namespace ApplicationLayer.Services.ApplicationServices.EntryItemsServices
{
	public interface IEntryItemsService : IBaseService
	{
		#region Create

		void AddEntryItems(EntryItemsDTO entryItemsDTO);
		void AddEntryItemsAsync(EntryItemsDTO entryItemsDTO);
		void AddRangeOfEntryItemss(IEnumerable<EntryItemsDTO> entryItemssDTO);
		void AddRangeOfEntryItemssAsync(IEnumerable<EntryItemsDTO> entryItemssDTO);

		#endregion

		#region Read

		EntryItemsDTO GetEntryItemsByID<IDType>(IDType id);
		EntryItemsDTO GetEntryItemsByIDAsync<IDType>(IDType id);
		EntryItemsDTO GetEntryItemsByFilter(Func<EntryItemsDTO, bool> filter);
		IEnumerable<EntryItemsDTO> GetAllEntryItemss();
		IEnumerable<EntryItemsDTO> GetAllEntryItemssByFilter(Func<EntryItemsDTO, bool> filter);

		#endregion

		#region Update

		void UpdateEntryItems<IDType>(IDType ID, EntryItemsDTO newEntryItems);

		#endregion

		#region Delete

		void DeleteEntryItems(EntryItemsDTO entryItemsDTO);
		void DeleteEntryItemsById(int id);

		void DeleteRangeOfEntryItemss(IEnumerable<EntryItemsDTO> entryItemssDTO);

		#endregion

		#region Additional Functionalities

		bool Exist(int Id);

		#endregion

	}
}
