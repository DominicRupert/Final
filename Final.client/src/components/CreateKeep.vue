<template>
  <Modal id="keep-modal">
    <template #modal-title
      >{{ keep.name }} by: {{ keep.creator?.name }}
    </template>
    <template #modal-body>
      <div class="container-fluid">
   
        
      </div>
    </template>
  </Modal>

</template>


<script>
import { AppState } from "../AppState";
import { computed, reactive, onMounted, onBeforeUnmount, ref } from "vue";
import { Modal } from "bootstrap";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
export default {
  props: { keep: { type: Object, reqiured: true } },
  setup() {
    const editable = ref({});

    return {
      editable,
      keep: computed(() => AppState.keeps),

      account: computed(() => AppState.account),
      profile: computed(() => AppState.profile),
      async createKeep() {
        try {
          await keepsService.createKeep(editable.keep);
        } catch (error) {
          logger.error(error);
        }
      },


    };
  },
};
</script>


<style lang="scss" scoped>
</style>