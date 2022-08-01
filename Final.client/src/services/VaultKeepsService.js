import { AppState } from "../AppState.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

class VaultKeepsService {
  async getVaultKeeps(id) {
    const res = await api.get(`api/vaults/${id}/keeps`);
AppState.activeKeep = res.data;
logger.log(AppState.activeKeep);

  }
    async createVaultKeeps(vaultKeep) {
      vaultKeep.keepId = AppState.activeKeep.id;
        const res = await api.post("api/vaultkeeps", vaultKeep);
        AppState.profileKeeps.push(res.data);
    }
    async deleteVaultKeeps(vaultKeepId) {
        const res = await api.delete(`api/vaultkeeps/${vaultKeepId}`);
    }

}

export const vaultKeepsService = new VaultKeepsService();