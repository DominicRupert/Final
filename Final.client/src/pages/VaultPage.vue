<template>
  <div class="masonry-frame">
    <div class="row">
      <div class="col-12">

        <!-- <h5 class="card-title">{{ keep.name }}</h5> -->
        <!-- <p class="card-text">{{ keep.description }}</p> -->
      </div>
    </div>
    <div class="row">
      <div class="col-12">
        <!-- <img :src="keep.img" class="img-fluid" :alt="keep.img" /> -->
      </div>
    </div>
  </div>

</template>


<script>
import { computed, onMounted } from "@vue/runtime-core";
import { AppState } from "../AppState";
import { keepsService } from "../services/KeepsService";
import { Modal } from "bootstrap";
import Pop from "../utils/Pop";
import { useRoute, useRouter } from "vue-router";
import { vaultKeepsService } from "../services/VaultKeepsService";
import { vaultsService } from "../services/VaultsService";
import { router } from "../router";
import { logger } from "../utils/Logger.js";
export default {
  props: {
    keep: {
      type: Object,
      required: true
    }
  },

  name: "Vault",
  setup(props) {
    const route = useRoute();
    const router = useRouter();
    onMounted(async () => {
      try {
        await vaultsService.getVaultById(route.params.id);
        await vaultKeepsService.getVaultKeeps(route.params.id);
        // await keepsService.getVaultKeeps(route.params.id);

      }
      catch (error) {
        logger.error(error);
        Pop.toast(error.message);
        router.push({ name: "Home" });
      }
    });
    return {
      vaults: computed(() => AppState.vaults),

      vault: computed(() => AppState.vaults.find(v => v.id === route.params.id)),
      keeps: computed(() => AppState.keeps),
      vaultKeeps: computed(() => AppState.vaultKeeps),
      vaults: computed(() => AppState.profileVaults),
      activeVault: computed(() => AppState.activeVault),
      profileVaultKeeps: computed(() => AppState.profileVaultKeeps),
      profileKeeps: computed(() => AppState.profileKeeps),
      profile: computed(() => AppState.profile),
      activeKeep: computed(() => AppState.activeKeep),

      async deleteVaults() {
        try {
          await vaultsService.deleteVaults(AppState.activeVault);
          Pop.toast("Vault gone");
          router.push({
            name: "profile",
            params: {
              id: props.activeVault.userId
            }
          });
        } catch (error) {
          logger.error(error);
          Pop.toast(error.message);
        }
      },
      async deleteVaultKeeps() {
        try {
          await vaultKeepsService.deleteVaultKeeps(AppState.activeVault);
          Pop.toast("VaultKeeps gone");
          router.push({
            name: "profile",
            params: {
              id: props.activeVault.userId
            }
          });
        } catch (error) {
          logger.error(error);
          Pop.toast(error.message);
        }
      },
    };
  }
}

</script>


<style lang="scss" scoped>
</style>