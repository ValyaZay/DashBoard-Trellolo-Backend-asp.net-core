using System;
using System.Collections.Generic;
using System.Text;
using TrelloProject.DAL.Entities;

namespace TrelloProject.DAL.Interfaces
{
    public interface IBoardRepository
    {
        Board GetBoard(int Id);
        IEnumerable<Board> GetAllBoards();
    }
}
